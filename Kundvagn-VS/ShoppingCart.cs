namespace Kundvagn_VS
{
  public class ShoppingCart<T> : IEnumerable<T> where T : Item
  {
    T[]? InternalStorage = new T[2];

    int FirstOpenSlot = 0;

    // Beräknar summan av kundvagnen.
    public decimal CalculatePrice(bool priceOnVAT)
    {
      decimal SummaPrice = 0;
      foreach (T ItemToCount in InternalStorage)
      {
        if (ItemToCount != null)
        {
          SummaPrice += (priceOnVAT ? ItemToCount.PriceVAT : ItemToCount.Price);
        }
      }
      return SummaPrice;
    }

    public T? GetMostExpensiveItem()
    {
      T? MostExp = null;
      decimal MaxPrice = 0;
      foreach (T ItemToMax in InternalStorage)
      {
        if ((ItemToMax != null) && (ItemToMax.Price > MaxPrice))
        {
          MaxPrice = ItemToMax.Price;
          MostExp = ItemToMax;
        }
      }
      return MostExp;
    }

    public void Clear()
    {
      for (int indexToClear = 0; indexToClear < InternalStorage.Length; indexToClear++)
      {
        InternalStorage[indexToClear] = null;
      }
      FirstOpenSlot = 0;
      GC.Collect();
    }

    public void DeliveryFree()
    {
      decimal TotalPrice = CalculatePrice(false);
      if (TotalPrice < 300)
      {
        Remove("Frakt");
        this.Add((T)new Item("Frakt", 75, "Paket frakt DHL"));
      }
    }

    // Loopar igenom en lista och kollar ledig plats dvs. null, om ingen finns då är FirstOpenSlot sist i listan.
    public void SetOpenSlot()
    {
      int indexFree;
      for (indexFree = 0; indexFree < InternalStorage.Length; indexFree++)
      {
        if (InternalStorage[indexFree] == null)
        {
          FirstOpenSlot = indexFree;
          return;
        }
      }
      FirstOpenSlot = InternalStorage.Length - 1;
    }

    // Tar bort en sträng i listan på position index.
    public void RemoveElementAt(int index)
    {
      if ((index >= 0) && (index < InternalStorage.Length))
      {
        InternalStorage[index] = null;
        GC.Collect();
        if (index < FirstOpenSlot)
        {
          FirstOpenSlot = index;
        }
      }
    }

    // Tar bort alla element i listan med strängen ItemToRemove
    public void Remove(string ItemToRemove)
    {
      int indexFree;
      if (!String.IsNullOrEmpty(ItemToRemove))
      {
        for (indexFree = InternalStorage.Length - 1; indexFree >= 0; indexFree--)
        {
          if ((InternalStorage[indexFree] != null) && (ItemToRemove == InternalStorage[indexFree].Name + ""))
          {
            InternalStorage[indexFree] = null;
            if (indexFree < FirstOpenSlot)
            {
              FirstOpenSlot = indexFree;
            }
          }
        }
        GC.Collect();
      }
    }

    public void Add(T ItemToAdd)
    {
      if (InternalStorage[FirstOpenSlot] == null)
      {
        InternalStorage[FirstOpenSlot] = ItemToAdd;
        SetOpenSlot();
      }
      else
      {
        T[] NewBagOfItems = new T[InternalStorage.Length + 1];
        Array.Copy(InternalStorage, NewBagOfItems, InternalStorage.Length);
        NewBagOfItems[InternalStorage.Length] = ItemToAdd;
        // this.Clear();
        FirstOpenSlot = InternalStorage.Length;
        InternalStorage = NewBagOfItems;
      }
    }

    public void PrintStorage(string inInfo, bool priceOnVAT)
    {
      int indexNr = 0;
      Console.WriteLine(inInfo);
      foreach (T varaNamn in InternalStorage)
      {
        Console.Write((indexNr++) + " ");
        if (varaNamn == null)
        {
          Console.WriteLine("Tom post");
        }
        else
        {
          Console.WriteLine(varaNamn.Name + "\t\t" + (priceOnVAT ? varaNamn.PriceVAT : varaNamn.Price) + "\t" + varaNamn.Decription);
        }
      }
      Console.WriteLine("--------------");
      Console.WriteLine("Antal poster :" + InternalStorage.Length);
      Console.WriteLine("Första lediga post är :" + FirstOpenSlot);
      Console.WriteLine("Summa belopp :" + CalculatePrice(true));
      Console.WriteLine("  ");
      Console.ReadLine();
    }

    public IEnumerator<T> GetEnumerator()
    {
      for (int index = 0; index < InternalStorage.Length; index++)
      {
        yield return InternalStorage[index];
      }
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }
  }
}
