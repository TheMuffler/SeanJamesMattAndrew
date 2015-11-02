using UnityEngine;
using System.Linq;
using System.Collections;

public class ActionBarCreator : MonoBehaviour {
	public ActionBarRow BottomBar;
	public ActionBarRow LeftBar;
	ActionBarDescriptor[] spellDescriptors = new ActionBarDescriptor[0];
	
	void Start()
	{
		spellDescriptors = new ActionBarDescriptor[16];
		
		for (int i = 0; i < spellDescriptors.Length; ++i)
		{
			spellDescriptors[i] = new ActionBarDescriptor
			{
				Atlas = 2,
				Icon = i,
				Callback = (d) =>
				{
					d.Cooldown = 5f;
				},
			};
		}
		
		BottomBar.AddInitCallback((row) => {
			row.SetButton(0, spellDescriptors[0]);
			row.SetButton(1, spellDescriptors[1]);
			row.SetButton(2, spellDescriptors[3]);
			row.SetButton(3, spellDescriptors[11]);
			row.SetButton(4, spellDescriptors[15]);
			row.SetButton(0, spellDescriptors[2]);
		});

	}
	
	void Update()
	{

	}
	
	void InitPotion(ActionBarRow row, int b, int n)
	{
		row.SetButton(b, new ActionBarDescriptor {
			Atlas = 3,
			Icon = n,
			ItemGroup = 1,
			ItemType = n,
			Stackable = true,
			Stack = 1,
			Callback = PotionClick
		});
	}
	
	void PotionClick(ActionBarDescriptor descriptor)
	{
		if (descriptor.Stack > 0)
		{
			descriptor.Stack -= 1;
			descriptor.Cooldown = 10;
			
			if (descriptor.Stack == 0)
			{
				foreach (ActionBarButton b in descriptor.Buttons.ToArray())
				{
					if (b.ItemGroup == descriptor.ItemGroup)
					{
						b.RemoveDescriptor();
					}
				}
			}
			else
			{
				
			}
		}
	}
	
	void BagClick(ActionBarDescriptor descriptor)
	{
		
	}
}
