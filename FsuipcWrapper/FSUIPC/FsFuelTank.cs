namespace FSUIPC;

public class FsFuelTank
{
	private FSFuelTanks tank;

	private double capacityUSGallons;

	private double levelFraction;

	private double poundsPerGallon;

	internal bool valueChanged;

	public bool IsPresent => capacityUSGallons > 0.0;

	public string Name => tank.ToString();

	public FSFuelTanks Tank => tank;

	public double CapacityUSGallons => capacityUSGallons;

	public double CapacityLitres => capacityUSGallons * 3.78541178;

	public double CapacityLbs => capacityUSGallons * poundsPerGallon;

	public double CapacityKgs => CapacityLbs * 0.45359237;

	public double CapacitySlugs => CapacityLbs * 0.0310809502;

	public double CapacityNewtons => CapacityLbs * 4.4482216282509;

	public double LevelPercentage
	{
		get
		{
			return levelFraction * 100.0;
		}
		set
		{
			levelFraction = value / 100.0;
			if (levelFraction > 1.0)
			{
				levelFraction = 1.0;
			}
			valueChanged = true;
		}
	}

	public double LevelUSGallons
	{
		get
		{
			return capacityUSGallons * levelFraction;
		}
		set
		{
			LevelPercentage = value / capacityUSGallons * 100.0;
		}
	}

	public double LevelLitres
	{
		get
		{
			return LevelUSGallons * 3.78541178;
		}
		set
		{
			LevelUSGallons = value / 3.78541178;
		}
	}

	public double WeightLbs
	{
		get
		{
			return LevelUSGallons * poundsPerGallon;
		}
		set
		{
			LevelUSGallons = value / poundsPerGallon;
		}
	}

	public double WeightKgs
	{
		get
		{
			return WeightLbs * 0.45359237;
		}
		set
		{
			WeightLbs = value / 0.45359237;
		}
	}

	public double WeightSlugs
	{
		get
		{
			return WeightLbs * 0.0310809502;
		}
		set
		{
			WeightLbs = value / 0.0310809502;
		}
	}

	public double WeightNewtons
	{
		get
		{
			return WeightLbs * 4.4482216282509;
		}
		set
		{
			WeightLbs = value / 4.4482216282509;
		}
	}

	internal FsFuelTank(FSFuelTanks Tank, double Capacity, double Level, double PoundsPerGallon)
	{
		tank = Tank;
		capacityUSGallons = Capacity;
		levelFraction = Level;
		poundsPerGallon = PoundsPerGallon;
	}

	internal void update(double Capacity, double Level, double PoundsPerGallon)
	{
		capacityUSGallons = Capacity;
		levelFraction = Level;
		poundsPerGallon = PoundsPerGallon;
		valueChanged = false;
	}
}
