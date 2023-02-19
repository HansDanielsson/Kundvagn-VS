namespace Kundvagn_VS
{
  public class Item
  {
    public string Name { get; set; }

    public decimal Price { get; set; }

    public decimal PriceVAT { get; set; }

    public string Decription { get; set; }

    public Item(string NameOfItem, decimal PriceOfItem, string DecriptionsOfItem)
    {
      Name = NameOfItem;
      Price = PriceOfItem;
      Decription = DecriptionsOfItem;
      PriceVAT = (decimal)1.25 * PriceOfItem;
    }

    public override string ToString()
    {
      return Name + "\t" + Price + "\t" + PriceVAT + "\t" + Decription;
    }
  }
}
