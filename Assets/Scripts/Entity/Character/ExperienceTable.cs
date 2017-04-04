using System;
using System.Text;

public class ExperienceTable
{
	private uint initialExperience;
	private uint maximumLevel;
	private float[] table;

	public ExperienceTable(uint initialExperience, uint maxLevel)
	{
		this.initialExperience = initialExperience;
		this.maximumLevel = maxLevel;
	}

	public void GenerateTable()
	{
		this.table = new float[this.maximumLevel];
		this.table[0] = this.initialExperience * 1.314f;
		for (uint i = 1; i < this.table.Length; ++i)
		{
			this.table[i] = ((this.table[i - 1] + this.initialExperience) * 1.314f).Clamp(0.0f, int.MaxValue);
		}
	}

	/// <summary>
	/// Generates an experience table using the function provided.
	/// Parameters: InitialExperience, MaximumLevel, Table
	/// </summary>
	public void GenerateTable(Func<uint, uint, float[], float> generationMethod)
	{
		this.table = new float[this.maximumLevel];
		this.table[0] = this.initialExperience * 1.314f;
		for (uint i = 1; i < this.table.Length; ++i)
		{
			this.table[i] = generationMethod(this.initialExperience, this.maximumLevel, this.table);
		}
	}

	public float GetExperienceToNextLevel(uint level)
	{
		--level;
		if (this.table != null && level < this.table.Length)
		{
			return this.table[level];
		}
		return 0.0f;
	}

	public override string ToString()
	{
		StringBuilder sb = new StringBuilder();
		sb.AppendLine();
		for (int i = 0; i < this.table.Length; ++i)
		{
			sb.AppendLine("Level[" + (i + 1) + "] " + ((uint)this.table[i]).ToString("N0"));
		}
		return sb.ToString();
	}
}