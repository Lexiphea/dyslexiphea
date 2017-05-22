using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterStat : MonoBehaviour, ICharacterStat
{
	private float baseValue = 0.0f;
	private float minValue = 0.0f;
	private float maxValue = 0.0f;
	private float modifier = 0.0f;
	private float totalValue = 0.0f;

	private Dictionary<Type, Func<ICharacterStat, float>> formulas = new Dictionary<Type, Func<ICharacterStat, float>>();
	private Dictionary<Type, ICharacterStat> parents = new Dictionary<Type, ICharacterStat>();
	private Dictionary<Type, ICharacterStat> children = new Dictionary<Type, ICharacterStat>();

	/// <summary>
	/// Returns the base value of the stat. Modifying this will trigger the parent stats to update.
	/// </summary>
	public float BaseValue
	{
		get
		{
			return this.baseValue;
		}
		set
		{
			if (this.baseValue != value)
			{
				this.baseValue = value;
				this.UpdateValues(true);
			}
		}
	}

	/// <summary>
	/// Returns the minimum value of the stat. Modifying this will trigger the parent stats to update.
	/// </summary>
	public float MinValue
	{
		get
		{
			return this.minValue;
		}
		set
		{
			if (this.minValue != value)
			{
				this.minValue = value;
				this.UpdateValues(true);
			}
		}
	}

	/// <summary>
	/// Returns the maximum value of the stat. Modifying this will trigger the parent stats to update.
	/// </summary>
	public float MaxValue
	{
		get
		{
			return this.maxValue;
		}
		set
		{
			if (this.maxValue != value)
			{
				this.maxValue = value;
				this.UpdateValues(true);
			}
		}
	}

	public abstract float InitialValue { get; }
	public abstract float DefaultMinValue { get; }
	public abstract float DefaultMaxValue { get; }

	/// <summary>
	/// Returns the current modifier value.
	/// </summary>
	public float Modifier
	{
		get
		{
			return this.modifier;
		}
	}

	/// <summary>
	/// Returns the total stat value clamped to the minimum and maximum value.
	/// </summary>
	public float TotalValue
	{
		get
		{
			return this.totalValue;
		}
	}

	public abstract Type[] ParentTypes { get; }
	public abstract Type[] ChildTypes { get; }

	public virtual void OnBeforeAwake()
	{
	}

	protected void Awake()
	{
		this.OnBeforeAwake();

		this.baseValue = InitialValue;
		this.minValue = DefaultMinValue;
		this.maxValue = DefaultMaxValue;

		//Get the other character stat components on the entity and set up heirarchy based on ParentTypes and ChildTypes.
		int i;
		if (ParentTypes != null)
		{
			for (i = 0; i < ParentTypes.Length; ++i)
			{
				CharacterStat parent = this.gameObject.GetComponent(ParentTypes[i]) as CharacterStat;
				if (parent != null)
				{
					parent.AddChild(this);
				}
			}
		}
		if (ChildTypes != null)
		{
			for (i = 0; i < ChildTypes.Length; ++i)
			{
				CharacterStat child = this.gameObject.GetComponent(ChildTypes[i]) as CharacterStat;
				if (child != null)
				{
					this.AddChild(child);
				}
			}
		}

		this.OnAfterAwake();
	}

	public virtual void OnAfterAwake()
	{
	}

	public virtual void OnBeforeDestroy()
	{
	}

	protected void OnDestroy()
	{
		this.OnBeforeDestroy();

		int i;
		if (ParentTypes != null)
		{
			for (i = 0; i < ParentTypes.Length; ++i)
			{
				CharacterStat parent = this.gameObject.GetComponent(ParentTypes[i]) as CharacterStat;
				if (parent != null)
				{
					parent.RemoveChild(this);
				}
			}
		}
		if (ChildTypes != null)
		{
			for (i = 0; i < ChildTypes.Length; ++i)
			{
				CharacterStat child = this.gameObject.GetComponent(ChildTypes[i]) as CharacterStat;
				if (child != null)
				{
					this.RemoveChild(child);
				}
			}
		}

		this.OnAfterDestroy();
	}

	public virtual void OnAfterDestroy()
	{
	}

	/// <summary>
	/// Re-calculates the TotalValue of the stat. Parents are updated if the old total is not equal to the new total.
	/// </summary>
	protected void UpdateValues()
	{
		this.UpdateValues(false);
	}
	/// <summary>
	/// Re-calculates the TotalValue of the stat. Parents are updated if the old total is not equal to the new total.
	/// </summary>
	protected void UpdateValues(bool forceUpdate)
	{
		float prevTotalValue = this.totalValue;
		this.modifier = this.CalculateModifier();
		this.totalValue = (this.baseValue + this.modifier).Clamp(this.minValue, this.maxValue);
		if (forceUpdate || this.totalValue != prevTotalValue)
		{
			foreach (CharacterStat parent in this.parents.Values)
			{
				parent.UpdateValues(); 
			}
		}
	}

	/// <summary>
	/// Calculates the modifier bonus.
	/// </summary>
	private float CalculateModifier()
	{
		float modifier = 0.0f;
		foreach (KeyValuePair<Type, Func<ICharacterStat, float>> pair in this.formulas)
		{
			ICharacterStat child;
			if (this.children.TryGetValue(pair.Key, out child))
			{
				//TODO: Maybe this should be checked to prevent the modifier from rolling over?
				modifier += pair.Value.Invoke(child);
			}
		}
		return modifier;
	}

	public void AddFormula(Type statType, Func<ICharacterStat, float> formula)
	{
		if (statType != null && formula != null && !this.formulas.ContainsKey(statType))
		{
			this.formulas.Add(statType, formula);
		}
	}
	public void RemoveFormula(Type statType)
	{
		this.formulas.Remove(statType);
	}

	private void AddParent(CharacterStat parent)
	{
		if (parent != null)
		{
			Type type = parent.GetType();
			if (!this.parents.ContainsKey(type))
			{
				this.parents.Add(type, parent);
			}
		}
	}
	private void RemoveParent(CharacterStat parent)
	{
		if (parent != null)
		{
			this.parents.Remove(parent.GetType());
		}
	}

	private void AddChild(CharacterStat child)
	{
		if (child != null)
		{
			Type type = child.GetType();
			if (!this.children.ContainsKey(type))
			{
				this.children.Add(type, child);
				child.AddParent(this);
				this.UpdateValues();
			}
		}
	}
	private void RemoveChild(CharacterStat child)
	{
		if (child != null)
		{
			this.children.Remove(child.GetType());
			child.RemoveParent(this);
			this.UpdateValues();
		}
	}
}