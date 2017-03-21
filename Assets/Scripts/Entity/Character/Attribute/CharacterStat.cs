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

	public float Modifier
	{
		get
		{
			return this.modifier;
		}
	}

	public float TotalValue
	{
		get
		{
			return this.totalValue;
		}
	}

	public abstract Type[] ParentTypes { get; }
	public virtual Type[] ChildTypes { get; }

	public virtual void Awake()
	{
		this.baseValue = InitialValue;
		this.minValue = DefaultMinValue;
		this.maxValue = DefaultMaxValue;

		int i;
		for (i = 0; i < ParentTypes.Length; ++i)
		{
			CharacterStat parent = this.gameObject.GetComponent(ParentTypes[i]) as CharacterStat;
			if (parent != null)
			{
				parent.AddChild(this);
			}
		}
		for (i = 0; i < ChildTypes.Length; ++i)
		{
			CharacterStat child = this.gameObject.GetComponent(ChildTypes[i]) as CharacterStat;
			if (child != null)
			{
				this.AddChild(child);
			}
		}
	}

	protected void UpdateValues()
	{
		this.UpdateValues(false);
	}
	protected void UpdateValues(bool forceUpdate)
	{
		float prevTotalValue = this.totalValue;
		this.ApplyChildren();
		if (forceUpdate || this.totalValue != prevTotalValue)
		{
			foreach (CharacterStat parent in this.parents.Values)
			{
				parent.UpdateValues(); 
			}
		}
	}

	private void ApplyChildren()
	{
		this.modifier = 0.0f;
		foreach (KeyValuePair<Type, Func<ICharacterStat, float>> pair in this.formulas)
		{
			ICharacterStat child;
			if (this.children.TryGetValue(pair.Key, out child))
			{
				this.modifier += pair.Value.Invoke(child);
			}
		}
		this.totalValue = (this.baseValue + this.modifier).Clamp(this.minValue, this.maxValue);
	}

	private void AddParent(CharacterStat parent)
	{
		Type type = parent.GetType();
		if (!this.parents.ContainsKey(type))
		{
			this.parents.Add(type, parent);
		}
	}
	private void RemoveParent(CharacterStat parent)
	{
		this.parents.Remove(parent.GetType());
	}

	private void AddChild(CharacterStat child)
	{
		Type type = child.GetType();
		if (!this.children.ContainsKey(type))
		{
			this.children.Add(type, child);
			child.AddParent(this);
			this.UpdateValues();
		}
	}
	private void RemoveChild(CharacterStat child)
	{
		this.children.Remove(children.GetType());
		child.RemoveParent(this);
		this.UpdateValues();
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
}