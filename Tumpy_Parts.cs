using System;
using System.Collections.Generic;
using UnityEngine;
using SimplePartLoader;
using SimplePartLoader.Features;
using Tumpy_Debug;
using System.Collections;
using NWH.VehiclePhysics2;
using NWH.VehiclePhysics2.Powertrain;
using NWH.WheelController3D;
using RVP;
using System.Linq;


namespace Tumpy_Parts
{
	public class ModMain : Mod
	{
		public override string ID => "Tumpy_Parts";
		public override string Name => ID;
		public override string Author => "Tumpy_Noodles";
		public override string Version => "0.7.1";
		public string Description => "Adds a Lot of Very \"Necessary\" Parts to the Game";
		public override byte[] Icon => Properties.Resources.Tumpy_Parts_Icon;

		public ModMain()
		{
			ThisMod = ModUtils.RegisterMod(this);
			verboseLogging = ThisMod.AddCheckboxToUI("Tumpy Parts Verbose Logging", "Verbose Logging", false);
			LogV.verboseLogging = verboseLogging.Checked;
			bundle = AssetBundle.LoadFromMemory(Properties.Resources.tumpy_parts);
			thumbnails = bundle.LoadAllAssets<Texture2D>();

			//INTERNAL PART GENERATION
			{
				//GENERAL
				{
					batteryHP = ThisMod.Load(bundle, "Tumpy Battery HP");
					originalName.Add("Battery");
					newParts.Add(batteryHP.Prefab);

					sparkplugsHP = ThisMod.Load(bundle, "Tumpy SparkPlugs HP");
					originalName.Add("SparkPlugs");
					newParts.Add(sparkplugsHP.Prefab);
				}

				//V8 HP
				{
					blockv872 = ThisMod.Load(bundle, "Tumpy CylinderBlock V8 7.2");
					originalName.Add("CylinderBlockV8");
					newParts.Add(blockv872.Prefab);

					coil07HP = ThisMod.Load(bundle, "Tumpy IgnitionCoil07 HP");
					originalName.Add("IgnitionCoil07");
					newParts.Add(coil07HP.Prefab);

					flywheel07LW = ThisMod.Load(bundle, "Tumpy FlyWheel07 LW");
					originalName.Add("Flywheel07");
					newParts.Add(flywheel07LW.Prefab);

					fuelpump07HP = ThisMod.Load(bundle, "Tumpy Fuelpump07 HP");
					originalName.Add("FuelPump07");
					newParts.Add(fuelpump07HP.Prefab);

					clutch07HP = ThisMod.Load(bundle, "Tumpy ClutchPlate07 HP");
					originalName.Add("ClutchPlate07");
					newParts.Add(clutch07HP.Prefab);

					harmonicbalancer07LW = ThisMod.Load(bundle, "Tumpy Harmonic Balancer07 LW");
					originalName.Add("Harmonic Balancer07");
					newParts.Add(harmonicbalancer07LW.Prefab);
				}

				//RADIATORS
				{
					radiator07LW = ThisMod.Load(bundle, "Tumpy Radiator07 LW");
					originalName.Add("Radiator07");
					newParts.Add(radiator07LW.Prefab);

					radiator06LW = ThisMod.Load(bundle, "Tumpy Radiator06 LW");
					originalName.Add("Radiator06");
					newParts.Add(radiator06LW.Prefab);

					radiator12LW = ThisMod.Load(bundle, "Tumpy Radiator12 LW");
					originalName.Add("Radiator12");
					newParts.Add(radiator12LW.Prefab);
				}

				//GEARBOX
				{
					gearbox06six = ThisMod.Load(bundle, "Tumpy Gearbox06 6");
					originalName.Add("GearBox06");
					newParts.Add(gearbox06six.Prefab);

					gearbox07six = ThisMod.Load(bundle, "Tumpy Gearbox07 6");
					originalName.Add("GearBox507");
					newParts.Add(gearbox07six.Prefab);

					gearbox12six = ThisMod.Load(bundle, "Tumpy Gearbox12 6");
					originalName.Add("GearBox125");
					newParts.Add(gearbox12six.Prefab);

					gearbox00six = ThisMod.Load(bundle, "Tumpy Gearbox00 6");
					originalName.Add("GearBox00");
					newParts.Add(gearbox00six.Prefab);
				}

				//DIFFS
				{
					diff1215 = ThisMod.Load(bundle, "Tumpy Diff12 1.5");
					originalName.Add("Diff12Open380");
					newParts.Add(diff1215.Prefab);

					diff1220 = ThisMod.Load(bundle, "Tumpy Diff12 2.0");
					originalName.Add("Diff12Open380");
					newParts.Add(diff1220.Prefab);

					diff1224 = ThisMod.Load(bundle, "Tumpy Diff12 2.4");
					originalName.Add("Diff12Open380");
					newParts.Add(diff1224.Prefab);

					diff1228 = ThisMod.Load(bundle, "Tumpy Diff12 2.8");
					originalName.Add("Diff12Open380");
					newParts.Add(diff1228.Prefab);

					diff1250 = ThisMod.Load(bundle, "Tumpy Diff12 5.0");
					originalName.Add("Diff12Open380");
					newParts.Add(diff1250.Prefab);

					diff1260 = ThisMod.Load(bundle, "Tumpy Diff12 6.0");
					originalName.Add("Diff12Open380");
					newParts.Add(diff1260.Prefab);

					diff0715 = ThisMod.Load(bundle, "Tumpy Diff07 1.5");
					originalName.Add("Diff07Open380");
					newParts.Add(diff0715.Prefab);

					diff0720 = ThisMod.Load(bundle, "Tumpy Diff07 2.0");
					originalName.Add("Diff07Open380");
					newParts.Add(diff0720.Prefab);

					diff0724 = ThisMod.Load(bundle, "Tumpy Diff07 2.4");
					originalName.Add("Diff07Open380");
					newParts.Add(diff0724.Prefab);

					diff0728 = ThisMod.Load(bundle, "Tumpy Diff07 2.8");
					originalName.Add("Diff07Open380");
					newParts.Add(diff0728.Prefab);

					diff0750 = ThisMod.Load(bundle, "Tumpy Diff07 5.0");
					originalName.Add("Diff07Open380");
					newParts.Add(diff0750.Prefab);

					diff0760 = ThisMod.Load(bundle, "Tumpy Diff07 6.0");
					originalName.Add("Diff07Open380");
					newParts.Add(diff0760.Prefab);

					diff0615 = ThisMod.Load(bundle, "Tumpy Diff06 1.5");
					originalName.Add("Diff06");
					newParts.Add(diff0615.Prefab);

					diff0620 = ThisMod.Load(bundle, "Tumpy Diff06 2.0");
					originalName.Add("Diff06");
					newParts.Add(diff0620.Prefab);

					diff0624 = ThisMod.Load(bundle, "Tumpy Diff06 2.4");
					originalName.Add("Diff06");
					newParts.Add(diff0624.Prefab);

					diff0628 = ThisMod.Load(bundle, "Tumpy Diff06 2.8");
					originalName.Add("Diff06");
					newParts.Add(diff0628.Prefab);

					diff0650 = ThisMod.Load(bundle, "Tumpy Diff06 5.0");
					originalName.Add("Diff06");
					newParts.Add(diff0650.Prefab);

					diff0660 = ThisMod.Load(bundle, "Tumpy Diff06 6.0");
					originalName.Add("Diff06");
					newParts.Add(diff0660.Prefab);
				}

				//I6D HP
				{
					blocki6d34 = ThisMod.Load(bundle, "Tumpy CylinderBlock I6D 3.4");
					originalName.Add("CylinderBlockI6D");
					newParts.Add(blocki6d34.Prefab);

					glowplugHP = ThisMod.Load(bundle, "Tumpy GlowPlug HP");
					originalName.Add("GlowPlug");
					newParts.Add(glowplugHP.Prefab);

					glowplugrelay12HP = ThisMod.Load(bundle, "Tumpy GlowPlugRelay12 HP");
					originalName.Add("GlowPlugRelay12");
					newParts.Add(glowplugrelay12HP.Prefab);

					airfilter12HP = ThisMod.Load(bundle, "Tumpy AirFilter12 HP");
					originalName.Add("AirFilter12");
					newParts.Add(airfilter12HP.Prefab);

					head12HP = ThisMod.Load(bundle, "Tumpy CylinderHead12 HP");
					originalName.Add("CylinderHead12");
					newParts.Add(head12HP.Prefab);
				}

				//I4 HP
				{
					flywheel06LW = ThisMod.Load(bundle, "Tumpy Flywheel06 LW");
					originalName.Add("Flywheel06");
					newParts.Add(flywheel06LW.Prefab);

					clutch06HP = ThisMod.Load(bundle, "Tumpy Clutch06 LW");
					originalName.Add("ClutchPlate");
					newParts.Add(clutch06HP.Prefab);

					head06HP = ThisMod.Load(bundle, "Tumpy CylinderHead06 HP");
					originalName.Add("CylinderHead06");
					newParts.Add(head06HP.Prefab);

					coil06HP = ThisMod.Load(bundle, "Tumpy IgnitionCoil06 HP");
					originalName.Add("IgnitionCoil");
					newParts.Add(coil06HP.Prefab);
				}

				//BIKE HP
				{
					camshaft00HP = ThisMod.Load(bundle, "Tumpy Camshaft00 HP");
					originalName.Add("Camshaft00");
					newParts.Add(camshaft00HP.Prefab);

					carburetor00HP = ThisMod.Load(bundle, "Tumpy Carburetor00 HP");
					originalName.Add("Carburetor00");
					newParts.Add(carburetor00HP.Prefab);

					crankshaft00HP = ThisMod.Load(bundle, "Tumpy Crankshaft00 HP");
					originalName.Add("Crankshaft00");
					newParts.Add(crankshaft00HP.Prefab);

					cylinder00HP = ThisMod.Load(bundle, "Tumpy Cylinder00 HP");
					originalName.Add("Cylinder00");
					newParts.Add(cylinder00HP.Prefab);

					head00HP = ThisMod.Load(bundle, "Tumpy CylinderHead00 HP");
					originalName.Add("CylinderHead00");
					newParts.Add(head00HP.Prefab);

					sparkwires00HP = ThisMod.Load(bundle, "Tumpy SparkplugWires00 HP");
					originalName.Add("SparkplugWires00");
					newParts.Add(sparkwires00HP.Prefab);

					camshaft0AHP = ThisMod.Load(bundle, "Tumpy Camshaft0A HP");
					originalName.Add("Camshaft0A");
					newParts.Add(camshaft0AHP.Prefab);

					carburetor0AHP = ThisMod.Load(bundle, "Tumpy Carburetor0A HP");
					originalName.Add("Carburetor0A");
					newParts.Add(carburetor0AHP.Prefab);

					crankshaft0AHP = ThisMod.Load(bundle, "Tumpy Crankshaft0A HP");
					originalName.Add("Crankshaft0A");
					newParts.Add(crankshaft0AHP.Prefab);

					cylinder10AHP = ThisMod.Load(bundle, "Tumpy Cylinder10A HP");
					originalName.Add("Cylinder10A");
					newParts.Add(cylinder10AHP.Prefab);

					cylinder20AHP = ThisMod.Load(bundle, "Tumpy Cylinder20A HP");
					originalName.Add("Cylinder20A");
					newParts.Add(cylinder20AHP.Prefab);

					head10AHP = ThisMod.Load(bundle, "Tumpy CylinderHead10A HP");
					originalName.Add("CylinderHead10A");
					newParts.Add(head10AHP.Prefab);

					head20AHP = ThisMod.Load(bundle, "Tumpy CylinderHead20A HP");
					originalName.Add("CylinderHead20A");
					newParts.Add(head20AHP.Prefab);

					rockers0AHP = ThisMod.Load(bundle, "Tumpy Rockers0A HP");
					originalName.Add("Rockers0A");
					newParts.Add(rockers0AHP.Prefab);

					sparkwires0AHP = ThisMod.Load(bundle, "Tumpy SparkplugWires0A HP");
					originalName.Add("SparkplugWires0A");
					newParts.Add(sparkwires0AHP.Prefab);

					headcover00Chrome = ThisMod.Load(bundle, "Tumpy CylinderHeadCover00 Chrome");
					originalName.Add("CylinderHeadCover00");
					newParts.Add(headcover00Chrome.Prefab);

					headcover0AChrome = ThisMod.Load(bundle, "Tumpy CylinderHeadCover0A Chrome");
					originalName.Add("CylinderHeadCover0A");
					newParts.Add(headcover0AChrome.Prefab);
				}

				// = ThisMod.Load(bundle, "");
				//originalName.Add("");
				//newParts.Add();
			}

			//PREFAB GEN
			{
				//BIKE WHEELS
				{
					bikeShockFLLong = ThisMod.Load(bundle, "Tumpy ShockAbsorberFL00 Long");

					bikeShockFRLong = ThisMod.Load(bundle, "Tumpy ShockAbsorberFR00 Long");

					bikeRim21Front = ThisMod.Load(bundle, "Tumpy_Bike_Rim_Front_21");

					bikeRim21Back = ThisMod.Load(bundle, "Tumpy_Bike_Rim_Rear_21");

					spoke5BikeRim19Front = ThisMod.Load(bundle, "Tumpy_Spoke_5_Rim_Front_19");

					spoke5BikeRim19Rear = ThisMod.Load(bundle, "Tumpy_Spoke_5_Rim_Rear_19");

					spoke5BikeRim21Front = ThisMod.Load(bundle, "Tumpy_Spoke_5_Rim_Front_21");

					spoke5BikeRim21Rear = ThisMod.Load(bundle, "Tumpy_Spoke_5_Rim_Rear_21");

					bikeTire21 = ThisMod.Load(bundle, "Tumpy Bike Tire R21");

					bikeTire21_L = ThisMod.Load(bundle, "Tumpy Bike Tire R21 L");
				}
			}

			//PRE MADE
			{
				//ENGINE
				{
					singleCarbBlower = ThisMod.Load(bundle, "Tumpy Single Carb Blower");

					triplePortScoop = ThisMod.Load(bundle, "Tumpy Triple Port Scoop");

					triplePortScoopFilter = ThisMod.Load(bundle, "Tumpy Triple Port Scoop Filter");

					blowerBeltSinglei6 = ThisMod.Load(bundle, "Tumpy Blower Belt Single I6");

					blowerBeltSinglev8 = ThisMod.Load(bundle, "Tumpy Blower Belt Single V8");

					blowerBeltSinglev8Perf = ThisMod.Load(bundle, "Tumpy Blower Belt Single V8 Perf");

					squishCleaner = ThisMod.Load(bundle, "Tumpy Squish Air Cleaner");

					squishFilter = ThisMod.Load(bundle, "Tumpy Squish Air Filter");

					tunnelRamv8 = ThisMod.Load(bundle, "Tumpy Tunnel Ram Intake V8");

					blowerCrankPulleyi6 = ThisMod.Load(bundle, "Tumpy I6 Blower Crank Pulley");

					intakei6Perf = ThisMod.Load(bundle, "Tumpy I6 Intake Manifold HP");

					filterExtension1 = ThisMod.Load(bundle, "Tumpy Filter Extension 1C");

					v8FanExtension = ThisMod.Load(bundle, "Tumpy V8 Fan Extension");

					pipeAirCleaner = ThisMod.Load(bundle, "Tumpy Pipe Air Cleaner");

					hiHatBase = ThisMod.Load(bundle, "Tumpy Hi Hat Base");

					hiHatFilter = ThisMod.Load(bundle, "Tumpy Hi Hat Filter");

					hiHatTop = ThisMod.Load(bundle, "Tumpy Hi Hat Top");
				}
			}

			bundle.Unload(false);
			//ThisMod.GenerateThumbnails();
		}

		public override void OnLoad()
		{
			new Log($@"{this.Name} Log
VERSION: {this.Version}
SETUP STARTED.
---------------------------
", true);

			GameObject itemParent = GameObject.Find("ItemParent");
			if (itemParent)
			{
				new Log(@"SURVIVAL MODE STARTED!!!
");
				isSurvival = true;
			}
			else
				isSurvival = false;

			if (!partsBuilt)
			{
				BuildCatalogPartsList();
				SetupMaterials();
				FixExistingParts();
				FindOriginalParts();
				BuildNewParts();
				FinishParts();
				UpdatePartsFitsToCar();
			}
			else
				new Log("Parts already Built.");

			if (isSurvival)
			{
				if (!addedToSurvival)
					AddToSurvival();
				else
					new Log("Parts already Added to Survival.");
			}

			new Log($@"
---------------------------
SETUP FINISHED.





");
		}

		//OLD FUNCTIONS
		/*public void BuildCatalogue()
		{
			GameObject scene = GameObject.Find("SceneManager");
			if (scene)
			{
				catalogue = scene.transform.GetChild(25).GetChild(3).GetChild(0).GetChild(0);
				if (catalogue)
				{
					
					int count = catalogue.childCount;
					new Log($@"Catalogue Found.

CATALOGUE COUNT = {count}
");
					for (int i = 0; i < count; i++)
					{
						catalogue.GetChild(i).gameObject.TryGetComponent<SHOPitem>(out SHOPitem shopItem);
						if (shopItem && shopItem.ITEM)
						{
							catalogueParts.Add(shopItem.ITEM);
							//new Log($"{shopItem.ITEM.name}\n");
						}
					}
				}
			}
		}*/
		/*public GameObject FindInListIndex(int i)
		{
			for (int j = 0; j < catalogueParts.Count; j++)
			{
				GameObject go = catalogueParts[j];
				if (go.name == originalName[i])
				{
					new Log($@"'{originalName[i]}' FOUND.
");
					return go;
				}   
			}
			new Log($@"'{originalName[i]}' NOT FOUND IN CATALOGUE!!!");
			return null;
		}*/

		//GATHER ALL PARTS IN THE GAME
		public void BuildCatalogPartsList()
		{
			GameObject partsParent = GameObject.Find("PartsParent");
			if (partsParent)
			{
				JunkPartsList partslist = partsParent.GetComponent<JunkPartsList>();
				GameObject[] parts = partslist.Parts;
				new Log($@"Parts List Count = {parts.Length}
");
				foreach (GameObject part in parts)
				{
					catalogueParts.Add(part);
				}
			}
		}

		//RETURNS A PART IN CATALOG WITH GIVEN NAME
		public GameObject FindInList(string s)
		{
			for (int i = 0; i < catalogueParts.Count; i++)
			{
				GameObject go = catalogueParts[i];
				if (go && go.name == s)
				{
					new Log($@"'{s}' FOUND.
");
					return go;
				}
			}
			new Log($@"

!!!'{s}' NOT FOUND!!!

");
			return null;
		}

		//SETUPS UP MATERIALS TO BE PLASTERED ON PARTS
		public void SetupMaterials()
		{
			chromeRed = new Material(PaintingSystem.GetChromeMaterial());
			chromeRed.name = "Chrome Red";
			chromeRed.color = new Color(0.5f, 0f, 0f);
			chrome = new Material(PaintingSystem.GetChromeMaterial());
			chromeShader = chrome.shader;
			GameObject go = FindInList("CrankshaftPulley07");
			if (go)
			{
				new Log("Dark Steel Found.");
				darkSteel = new Material(go.GetComponent<MeshRenderer>().material);
				//new Log("Material Setup.");
			}
			go = FindInList("InjectorHP");
			if (go)
			{
				new Log("Metallic Orange Found.");
				metallicOrange = new Material(go.GetComponent<MeshRenderer>().materials[2]);
				metallicRed = new Material(go.GetComponent<MeshRenderer>().materials[2]);
				metallicRed.color = new Color(0.25f, 0f, 0f);
				//new Log("Materials Setup.");
			}
			go = FindInList("SparkplugWiresHP07");
			if (go)
			{
				new Log("Yellow Found.");
				yellow = new Material(go.GetComponent<MeshRenderer>().material);
			}
			new Log(@"Materials Setup Finsished.
");
		}

		//EXISTING PART FIXES
		public void FixExistingParts()
		{
			Material[] mats = new Material[] { };
			GameObject go = FindInList("VentilatorI6");
			if (go)
			{
				go.GetComponent<Partinfo>().Engine = true;
				new Log(@"Fan I6 Fixed.
");
			}
			go = FindInList("CylinderBlockV8");
			if (go)
			{
				go.GetComponent<CarProperties>().PartNameExtension = "5.6L V8";
				new Log(@"Block5.6 V8 Fixed.
");
			}
			go = FindInList("FuelPumpHP06");
			if (go)
			{
				go.GetComponent<CarProperties>().PartName = "FuelPump";
				go.GetComponent<CarProperties>().PartNameExtension = "HP";
				mats = go.GetComponent<MeshRenderer>().materials;
				mats[0] = chromeRed;
				go.GetComponent<MeshRenderer>().materials = mats;
				foreach (Texture2D t in thumbnails)
				{
					if (t.name == "Tumpy FuelPump06 HP")
					{
						thumbnail = t;
					}
				}
				go.GetComponent<Partinfo>().Thumbnail = thumbnail;
				new Log(@"FuelPump06 HP Fixed.
");
			}
			go = FindInList("DistributorHP06");
			if (go)
			{
				mats = go.GetComponent<MeshRenderer>().materials;
				mats[1].color = new Color(0.65f, 0.50f, 0f);
				mats[1].SetFloat("_Metallic", 0.5f);
				go.GetComponent<MeshRenderer>().materials = mats;
				foreach (Texture2D t in thumbnails)
				{
					if (t.name == "Tumpy Distributor06 HP")
					{
						thumbnail = t;
					}
				}
				go.GetComponent<Partinfo>().Thumbnail = thumbnail;
				new Log(@"Distributor06 HP Fixed.
");
			}
			go = FindInList("Turbocharger06");
			if (go)
			{
				CarProperties car = go.GetComponent<CarProperties>();
				car.Power = 2;
				new Log(@"TurboCharger06 Fixed.
");
			}
			go = FindInList("RimR19");
			if (go)
			{
				CarProperties car = go.GetComponent<CarProperties>();
				car.DMGAnyDamag = false;
				new Log(@"RimR19 Fixed.
");
			}
			go = FindInList("RimF19");
			if (go)
			{
				CarProperties car = go.GetComponent<CarProperties>();
				car.DMGAnyDamag = false;
				new Log(@"RimF19 Fixed.
");
			}
			go = FindInList("HeadlightCase00");
			if (go)
			{
				go.AddComponent<UnbreakableLight>();
				new Log(@"Bike Headlight Fixed.
");
			}
		}

		//BUILDS THE LIST OF PARTS THAT GET COPIED FROM TO BUILD NEW PARTS 
		public void FindOriginalParts()
		{
			for (int i = 0; i < originalName.Count; i++)
			{
				originalParts.Add(FindInList(originalName[i]));
			}
		}

		//COPIES EXISTING PART STUFF ON NEW PART STUFF FOR PARTS SIMILAR TO EXISTING PARTS
		public void BuildNewParts()
		{
			for (int i = 0; i < originalParts.Count; i++)
			{
				new LogV("BuildNewParts Ran.");
				if (i < newParts.Count)
				{
					new LogV("Has New Part Counterpart.");
					GameObject newGo = newParts[i];
					GameObject oldGo = originalParts[i];
					if (newGo && oldGo)
					{
						try
						{
							new LogV($@"New & Old Mesh Found.
New Mesh = {newGo.name}
Old Mesh = {oldGo.name}");
							int count = oldGo.transform.childCount;
							new LogV($"Child Count = {count}");
							if (count > 0)
							{
								for (int j = 0; j < count; j++)
								{
									new LogV($"Child Run {j}");
									Transform childGo = GameObject.Instantiate(oldGo.transform.GetChild(j));
									childGo.name = oldGo.transform.GetChild(j).name;
									childGo.transform.SetParent(newGo.transform);
								}
							}
							if (oldGo.GetComponent<MeshFilter>() && oldGo.GetComponent<MeshFilter>().mesh != null)
							{
								newGo.GetComponent<MeshFilter>().mesh = oldGo.GetComponent<MeshFilter>().mesh;
								new LogV("Mesh Found.");
							}
							if (oldGo.GetComponent<MeshRenderer>() && oldGo.GetComponent<MeshRenderer>().materials.Length > 0)
							{
								newGo.GetComponent<MeshRenderer>().materials = oldGo.GetComponent<MeshRenderer>().materials;
								new LogV("Materials Found.");
							}
							if (oldGo.GetComponent<MeshCollider>() && oldGo.GetComponent<MeshCollider>().sharedMesh != null)
							{
								newGo.GetComponent<MeshCollider>().sharedMesh = oldGo.GetComponent<MeshCollider>().sharedMesh;
								new LogV("Collision Found.");
							}
							new LogV("Art Setup Finished.");
							CarProperties carNew = newGo.GetComponent<CarProperties>();
							CarProperties carOld = oldGo.GetComponent<CarProperties>();
							if (carOld && carNew)
							{
								carNew.damagedMesh = carOld.damagedMesh;
								carNew.WornMaterial = carOld.WornMaterial;
								carNew.OldMaterial = carOld.OldMaterial;
								carNew.RuinedMaterial = carOld.RuinedMaterial;
								carNew.NormalMesh = carOld.NormalMesh;
								carNew.ChromeMat = PaintingSystem.GetChromeMaterial();
								if (oldGo.GetComponent<GearProfile>() != null)
								{
									GearProfile gear = newGo.AddComponent<GearProfile>();
									GearProfile gear1 = newGo.AddComponent<GearProfile>();
									GearProfile gear2 = newGo.AddComponent<GearProfile>();
									GearProfile gear3 = newGo.AddComponent<GearProfile>();
									if (carOld.TransmissionGearingProfile?.forwardGears.Count > 0)
									{
										gear.forwardGears = new List<float>(carOld.TransmissionGearingProfile?.forwardGears);
										gear1.forwardGears = gear.forwardGears;
										gear2.forwardGears = new List<float>(carOld.TransmissionGearingbroken2?.forwardGears);
										gear3.forwardGears = gear.forwardGears;
									}
									if (carOld.TransmissionGearingProfile?.reverseGears.Count > 0)
									{
										gear.reverseGears = new List<float>(carOld.TransmissionGearingProfile?.reverseGears);
										gear1.reverseGears = new List<float>(carOld.TransmissionGearingbroken1?.reverseGears);
										gear2.reverseGears = gear.reverseGears;
										gear3.reverseGears = gear.reverseGears;
									}
									gear1.Profilenumber = 1;
									gear2.Profilenumber = 2;
									gear3.Profilenumber = 3;
									carNew.TransmissionGearingProfile = gear;
									carNew.TransmissionGearingbroken1 = gear1;
									carNew.TransmissionGearingbroken2 = gear2;
									carNew.TransmissionGearingbroken3 = gear3;
									new LogV("GearBox Setup.");
								}
								new LogV("Car Properties Setup.");
							}
							Partinfo partNew = newGo.GetComponent<Partinfo>();
							Partinfo partOld = oldGo.GetComponent<Partinfo>();
							if (partOld && partNew)
							{
								if (partNew.FitsToCar.Length == 0)
									partNew.FitsToCar = partOld.FitsToCar;
								if (partNew.FitsToEngine.Length == 0)
									partNew.FitsToEngine = partOld.FitsToEngine;
								if (partNew.Thumbnail == null)
								{
									partNew.Thumbnail = partOld.Thumbnail;
								}
								new LogV("Partinfo Setup.");
							}
							new Log($@"{newParts[i].name} Built.");
						}
						catch (Exception e)
						{
							new Log($@"

!!!{newParts[i].name.ToUpper()} CANNOT BE BUILT!!!
{e}

");
						}
					}
					else
						new Log($@"

!!!{newParts[i].name.ToUpper()} CANNOT BE BUILT!!!
!!!OBJECT NOT FOUND!!!

");
					new Log();
				}
			}
			partsBuilt = true;
		}

		//FINAL TOUCHES ON PARTS. 
		public void FinishParts()
		{
			new Log(@"FinishParts Ran.
");
			Material[] mats = new Material[] { };
			Transform[] trans = new Transform[] { };
			Material mat = null;
			GameObject go = null;

			//GENERAL
			{
				batteryHP.Renderer.material.color = new Color(0.25f, 0f, 0f);
				new Log(@"Battery HP Finished.
");
				//SPARKPLUG HP BOX
				if (!isSurvival)
				{
					go = GameObject.Find("UnloadablesMain/shop/Shop/SHOPITEMS/SparkPlugHP");
					if (go)
					{
						go = go.GetComponent<SaleItem>().Item;
						go.GetComponent<Partinfo>().AllowFall = false;
						new Log("SparkPlugs HP Found & Fixed.");
						SaveItem save = sparkplugsHP.Prefab.AddComponent<SaveItem>();
						PickupTool tool = sparkplugsHP.Prefab.AddComponent<PickupTool>();
						UnityEngine.Object.Destroy(sparkplugsHP.GetComponent<CarProperties>());
						save.pickuptool = tool;
						save.PrefabName = "Tumpy SparkPlugs HP";
						tool.Box = true;
						tool.CanPutInBox = true;
						tool.InBoxprefab = go;
						tool.InBox = 4;
					}
					sparkplugsHP.Prefab.name = "SparkPlugs HP";
					sparkplugsHP.Prefab.layer = 19;
					sparkplugsHP.Prefab.tag = "Item";
					sparkplugsHP.Prefab.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(0.65f, 0.50f, 0f);
					new Log(@"SparkPlugs HP Finished.
");
				}
			}

			//V8 HP
			{
				CommonFixes.FixPart(blockv872, FixType.Dipstick);
				SPL.UpdateTransparentsReference(blockv872);
				new Log(@"CylinderBlock V8 7.2 Finished.
");
				mat = coil07HP.Renderer.materials[0];
				mat.color = new Color(0.25f, 0f, 0f);
				mat.SetFloat("_Metallic", 0.5f);
				mat.SetFloat("_Glossiness", 1f);
				new Log(@"IgnitionCoil07 HP Finished.
");
				flywheel07LW.Renderer.materials[0] = chrome;
				flywheel07LW.CarProps.Chromed = true;
				SPL.UpdateTransparentsReference(flywheel07LW);
				flywheel07LW.Prefab.AddComponent<RPMBoost>();
				new Log(@"FlyWheel07 LW Finished.
");
				fuelpump07HP.Renderer.materials[0] = chrome;
				fuelpump07HP.CarProps.Chromed = true;
				new Log(@"Fuelpump07 HP Finished.
");
				Material mat2 = clutch07HP.Renderer.materials[0];
				Material mat3 = clutch07HP.Renderer.materials[1];
				Material mat4 = clutch07HP.Renderer.materials[2];
				mat2.color = new Color(0.50f, 0.25f, 0f);
				mat3.SetFloat("_Metallic", 1f);
				mat3.SetFloat("_Glossiness", 1f);
				mat4.SetFloat("_Metallic", 1f);
				mat4.SetFloat("_Glossiness", 1f);
				new Log(@"ClutchPlate07 HP Finished.
");
				harmonicbalancer07LW.Renderer.material = darkSteel;
				new Log(@"Harmonic Balancer07 LW Finsihed.
");
			}

			//RADIATOR
			{
				Material mat5 = radiator07LW.Renderer.materials[1];
				mat5.mainTexture = null;
				mat5.SetFloat("_Metallic", 1f);
				mat5.SetFloat("_Glossiness", 1f);
				radiator07LW.Prefab.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = chrome;
				CommonFixes.FixPart(radiator07LW, FixType.Radiator);
				radiator07LW.CarProps.Coolant = radiator07LW.Prefab.GetComponentInChildren<FLUID>();
				ANYdamage dmg = radiator07LW.Prefab.AddComponent<ANYdamage>();
				dmg.Radiator = true;
				new Log(@"Radiator07 LW Finished.
");
				Material mat6 = radiator12LW.Renderer.materials[1];
				Material mat7 = radiator12LW.Renderer.materials[2];
				mat6.shader = chromeShader;
				mat6.SetTexture("_BumpMap", null);
				mat6.color = Color.white;
				mat6.SetFloat("_Metallic", 1f);
				mat6.SetFloat("_Glossiness", 1f);
				mat7.mainTexture = null;
				mat7.color = Color.white;
				mat7.SetFloat("_Metallic", 1f);
				mat7.SetFloat("_Glossiness", 1f);
				radiator12LW.Prefab.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = chrome;
				CommonFixes.FixPart(radiator12LW, FixType.Radiator);
				radiator12LW.CarProps.Coolant = radiator12LW.Prefab.GetComponentInChildren<FLUID>();
				dmg = radiator12LW.Prefab.AddComponent<ANYdamage>();
				dmg.Radiator = true;
				new Log(@"Radiator12 LW Finished.
");
				Material mat8 = radiator06LW.Renderer.materials[1];
				mat8.shader = chromeShader;
				mat8.color = Color.white;
				mat8.SetFloat("_Metallic", 1f);
				mat8.SetFloat("_Glossiness", 1f);
				radiator06LW.Prefab.transform.GetChild(6).GetChild(0).GetComponent<MeshRenderer>().material = chrome;
				CommonFixes.FixPart(radiator06LW, FixType.Radiator);
				radiator06LW.CarProps.Coolant = radiator06LW.Prefab.GetComponentInChildren<FLUID>();
				dmg = radiator06LW.Prefab.AddComponent<ANYdamage>();
				dmg.Radiator = true;
				new Log(@"Radiator06 LW Finished.
");
			}

			//GEARBOXES
			{
				if (gearbox06six.CarProps.TransmissionGearingProfile?.forwardGears.Count < 6)
				{
					gearbox06six.CarProps.TransmissionGearingProfile.forwardGears.Add(0.62f);
					gearbox06six.CarProps.TransmissionGearingbroken2.forwardGears.Add(0.62f);
					new Log(@"Gearbox06 Six Finished.
");
				}
				if (gearbox07six.CarProps.TransmissionGearingProfile?.forwardGears.Count < 6)
				{
					gearbox07six.CarProps.TransmissionGearingProfile.forwardGears.Add(0.5f);
					gearbox07six.CarProps.TransmissionGearingbroken2.forwardGears.Add(0.5f);
					new Log(@"Gearbox07 Six Finsihed.
");
				}
				if (gearbox12six.CarProps.TransmissionGearingProfile?.forwardGears.Count < 6)
				{
					gearbox12six.CarProps.TransmissionGearingProfile.forwardGears.Add(0.56f);
					gearbox12six.CarProps.TransmissionGearingbroken2.forwardGears.Add(0.56f);
					new Log(@"Gearbox12 Six Finished.
");
				}
				if (gearbox00six.CarProps.TransmissionGearingProfile?.forwardGears.Count > 6)
				{
					List<float> list = new List<float>() { 2.66f, 1.91f, 1.39f, 1f, 0.75f, 0.5f };
					gearbox00six.CarProps.TransmissionGearingProfile.forwardGears = list;
					gearbox00six.CarProps.TransmissionGearingbroken1.forwardGears = list;
					gearbox00six.CarProps.TransmissionGearingbroken2.forwardGears = list;
					gearbox00six.CarProps.TransmissionGearingbroken3.forwardGears = list;
					gearbox00six.CarProps.TransmissionGearingProfile.reverseGears.Clear();
					gearbox00six.CarProps.TransmissionGearingbroken1.reverseGears.Clear();
					gearbox00six.CarProps.TransmissionGearingbroken2.reverseGears.Clear();
					gearbox00six.CarProps.TransmissionGearingbroken3.reverseGears.Clear();
					new Log(@"Gearbox00 Six Finished.
");
				}
			}

			//I6D HP
			{
				CommonFixes.FixPart(blocki6d34, FixType.Dipstick);
				SPL.UpdateTransparentsReference(blocki6d34);
				blocki6d34.CarProps.DieselEngine = true;
				new Log(@"CylinderBlock I6D 3.4 Finished.
");
				glowplugHP.CarProps.GlowPlug = true;
				mats = glowplugHP.Renderer.materials;
				mats[1] = metallicRed;
				glowplugHP.Renderer.materials = mats;
				new Log(@"Glowplug HP Finished.
");
				glowplugrelay12HP.CarProps.GlowPlugRelay = true;
				mats = glowplugrelay12HP.Renderer.materials;
				mats[0].color = new Color(0.65f, 0.50f, 0f);
				mats[1].color = new Color(0.65f, 0.50f, 0f);
				glowplugrelay12HP.Renderer.materials = mats;
				new Log(@"Glowplug Relay12 HP Finished.
");
				Material mat12 = airfilter12HP.Renderer.material;
				mat12.color = new Color(0.25f, 0f, 0f);
				new Log(@"Airfilter12 HP Finished.
");
				mats = head12HP.Renderer.materials;
				mats[0] = chrome;
				head12HP.Renderer.materials = mats;
				head12HP.CarProps.Chromed = true;
				new Log(@"CylinderHead12 HP Finished.
");
			}

			//I4 HP
			{
				SPL.UpdateTransparentsReference(flywheel06LW);
				flywheel06LW.Renderer.material = chrome;
				flywheel06LW.CarProps.Chromed = true;
				flywheel06LW.Prefab.AddComponent<RPMBoost>();
				new Log(@"Flywheel06 LW Finished.
");
				mats = clutch06HP.Renderer.materials;
				mats[0].color = new Color(0.885f, 0.365f, 0f);
				mats[2].color = Color.red;
				clutch06HP.Renderer.materials = mats;
				new Log(@"Clutch06 HP Finsihed.
");
				head06HP.Renderer.material = chrome;
				head06HP.CarProps.Chromed = true;
				new Log(@"CylinderHead06 HP Finished.
");
				mats = coil06HP.Renderer.materials;
				mats[0] = metallicOrange;
				coil06HP.Renderer.materials = mats;
				new Log(@"IgnitionCoil06 HP Finished.
");
			}

			//BIKE ENIGNE PARTS
			{
				head00HP.Renderer.material = chrome;
				new Log(@"CylinderHead00 HP Finished.
");
				cylinder00HP.Renderer.material = chrome;
				SPL.UpdateTransparentsReference(cylinder00HP);
				new Log(@"Cylinder00 HP Finished.
");
				head10AHP.Renderer.material = chrome;
				new Log(@"CylinderHead10A HP Finished.
");
				head20AHP.Renderer.material = chrome;
				new Log(@"CylinderHead20A HP Finished.
");
				cylinder10AHP.Renderer.material = chrome;
				SPL.UpdateTransparentsReference(cylinder10AHP);
				new Log(@"Cylinder10A HP Finished.
");
				cylinder20AHP.Renderer.material = chrome;
				SPL.UpdateTransparentsReference(cylinder20AHP);
				new Log(@"Cylinder20A HP Finished.
");
				carburetor00HP.Renderer.material = chromeRed;
				new Log(@"Carburetor00 HP Finished.
");
				carburetor0AHP.Renderer.material = chromeRed;
				new Log(@"Carburetor0A HP Finished.
");
				mat = crankshaft00HP.Renderer.material;
				mat.color = new Color(0.5f, 0.5f, 0.5f);
				mat.SetFloat("_Glossiness", 1f);
				new Log(@"Crankshaft00 HP Finished.
");
				mat = crankshaft0AHP.Renderer.material;
				mat.color = new Color(0.5f, 0.5f, 0.5f);
				mat.SetFloat("_Glossiness", 1f);
				new Log(@"Crankshaft0A HP Finished.
");
				mat = camshaft00HP.Renderer.material;
				mat.color = new Color(0.5f, 0.5f, 0.5f);
				mat.SetFloat("_Glossiness", 1f);
				new Log(@"Camshaft00 HP Finished.
");
				mat = camshaft0AHP.Renderer.material;
				mat.color = new Color(0.5f, 0.5f, 0.5f);
				mat.SetFloat("_Glossiness", 1f);
				new Log(@"Camshaft0A HP Finished.
");
				sparkwires00HP.Renderer.material = yellow;
				sparkwires00HP.UseHandAttachment();
				new Log(@"SparkPlugWires00 HP Finished.
");
				sparkwires0AHP.Renderer.material = yellow;
				sparkwires0AHP.UseHandAttachment();
				new Log(@"SparkPlugWires0A HP Finished.
");
				mat = rockers0AHP.Renderer.material;
				mat.SetFloat("_Glossiness", 1f);
				new Log(@"Rockers0A HP Finished.
");
				headcover00Chrome.Renderer.material = chrome;
				new Log(@"ValveCover00 Chrome Finished.
");
				headcover0AChrome.Renderer.material = chrome;
				new Log(@"ValveCover0A Chrome Finished.
");
			}

			//BIKE WHEELS
			{
				bikeShockFLLong.CarProps.PartNameExtension = "FL Long";
				bikeShockFLLong.CarProps.SpringLength = 0.5f;
				trans = bikeShockFLLong.GetTransforms();
				Vector3 newPos = new Vector3(0, -0.94f, 0.0663f);
				foreach (Transform t in trans)
				{
					if (t.name == "LowerSHockL")
					{
						t.localPosition = newPos;
					}
				}
				bikeShockFLLong.Prefab.AddComponent<SpringExtension>();
				new Log(@"ShockAbsorber00FL Long Finished.
");
				bikeShockFRLong.CarProps.PartNameExtension = "FR Long";
				trans = bikeShockFRLong.GetTransforms();
				foreach (Transform t in trans)
				{
					if (t.name == "loweShock")
					{
						t.localPosition = newPos;
					}
					if (t.name == "RimF")
					{
						t.localPosition = newPos;
					}
				}
				new Log(@"ShockAbsorber00FR Long Finished.
");
				bikeRim21Front.CarProps.RimR = 21;
				bikeRim21Front.CarProps.PartNameExtension = "Front";
				bikeRim21Front.CarProps.DMGAnyDamag = false;
				trans = bikeRim21Front.GetTransforms();
				foreach (Transform t in trans)
				{
					if (t.name == "Tire19")
					{
						t.name = "Tire21";
					}
					if (t.name == "TireValve")
					{
						t.localPosition = new Vector3(0f, -0.23f, -0.0417f);
					}
				}
				new Log(@"New Bike Rim 21 Finished.
");
				bikeRim21Back.CarProps.RimR = 21;
				bikeRim21Back.CarProps.PartNameExtension = "Rear";
				bikeRim21Back.CarProps.DMGAnyDamag = false;
				trans = bikeRim21Back.GetTransforms();
				foreach (Transform t in trans)
				{
					if (t.name == "Tire19")
					{
						t.name = "Tire21";
					}
					if (t.name == "TireValve")
					{
						t.localPosition = new Vector3(0f, -0.23f, -0.0417f);
					}
				}
				new Log(@"New Bike Rim 21 Finished.
");
				bikeTire21.CarProps.RimR = 21;
				new Log(@"Bike Tire 21 Finished.
");
				bikeTire21_L.CarProps.RimR = 21;
				bikeTire21_L.CarProps.PartNameExtension = " L";
				new Log(@"Bike Tire 21 L Finished.
");
				spoke5BikeRim19Front.CarProps.PartNameExtension = "Front";
				spoke5BikeRim19Front.CarProps.DMGAnyDamag = false;
				new Log(@"Spoke 5 Rim Front 19 Finished.
");
				spoke5BikeRim19Rear.CarProps.PartNameExtension = "Rear";
				spoke5BikeRim19Rear.CarProps.DMGAnyDamag = false;
				new Log(@"Spoke 5 Rim Rear 19 Finished.
");
				spoke5BikeRim21Front.CarProps.RimR = 21;
				spoke5BikeRim21Front.CarProps.PartNameExtension = "Front";
				spoke5BikeRim21Front.CarProps.DMGAnyDamag = false;
				trans = spoke5BikeRim21Front.GetTransforms();
				foreach (Transform t in trans)
				{
					if (t.name == "Tire19")
					{
						t.name = "Tire21";
					}
					if (t.name == "TireValve")
					{
						t.localPosition = new Vector3(0f, -0.23f, -0.0417f);
					}
				}
				new Log(@"Spoke 5 Rim Front 21 Finished.
");
				spoke5BikeRim21Rear.CarProps.RimR = 21;
				spoke5BikeRim21Rear.CarProps.PartNameExtension = "Rear";
				spoke5BikeRim21Rear.CarProps.DMGAnyDamag = false;
				trans = spoke5BikeRim21Rear.GetTransforms();
				foreach (Transform t in trans)
				{
					if (t.name == "Tire19")
					{
						t.name = "Tire21";
					}
					if (t.name == "TireValve")
					{
						t.localPosition = new Vector3(0f, -0.23f, -0.0417f);
					}
				}
				new Log(@"Spoke 5 Rim Rear 21 Finished.
");
			}

			//ENGINE PARTS
			{
				blowerBeltSinglei6.UseHandAttachment();
				new Log(@"Blower Belt 1C I6 Finished.
");
				blowerBeltSinglev8.UseHandAttachment();
				new Log(@"Blower Belt 1C V8 Finished.
");
				blowerBeltSinglev8Perf.UseHandAttachment();
				new Log(@"Blower Belt 1C V8 Perf Finished.
");
				go = FindInList("IntakeManifoldPerf07");
				if (go)
				{
					mat = go.transform.GetChild(0).GetComponent<MeshRenderer>().material;
					if (mat)
						tunnelRamv8.Prefab.transform.GetChild(2).GetComponent<MeshRenderer>().material = mat;
					for (int i = 0; i < 10; i++)
					{
						GameObject child = GameObject.Instantiate(go.transform.GetChild(i).gameObject);
						child.name = $"BOLT_{i + 2}";
						child.transform.SetParent(tunnelRamv8.Prefab.transform, false);
					}
				}
				new Log(@"Tunnel Ram Intake V8 Finished.
");
				filterExtension1.UseHandAttachment();
				new Log(@"Air Filter Extension 1C Finsished.
");
				v8FanExtension.UseHandAttachment();
				new Log(@"V8 Fan Extension Finished.
");

			}

			new Log("FinishParts Finished. Yay!");

		}

		//UPDATES FITSTOCAR LIST ON NEW PARTS. MADE FOR GAME UPDATES AND MOD COMPATIBILITY
		public void UpdatePartsFitsToCar()
		{
			GameObject go = FindInList("CylinderBlockV8");
			if (go)
			{
				Partinfo v8part = go.GetComponent<Partinfo>();
				List<string> v8carlist = v8part.FitsToCar?.ToList<string>();
				//foreach (string s in v8carlist)
				//new Log(s);
				//new Log();
				UpdateFitsToCar(singleCarbBlower, v8carlist);
				//UpdateFitsToCar(blowerBeltDoublei6, v8carlist);
				UpdateFitsToCar(blowerBeltSinglei6, v8carlist);
				UpdateFitsToCar(blowerBeltSinglev8, v8carlist);
				UpdateFitsToCar(blowerBeltSinglev8Perf, v8carlist);
				UpdateFitsToCar(triplePortScoop, v8carlist);
				UpdateFitsToCar(triplePortScoopFilter, v8carlist);
				UpdateFitsToCar(squishCleaner, v8carlist);
				UpdateFitsToCar(squishFilter, v8carlist);
				UpdateFitsToCar(tunnelRamv8, v8carlist);
				UpdateFitsToCar(blowerCrankPulleyi6, v8carlist);
				UpdateFitsToCar(intakei6Perf, v8carlist);
				UpdateFitsToCar(filterExtension1, v8carlist);
				UpdateFitsToCar(v8FanExtension, v8carlist);
				UpdateFitsToCar(pipeAirCleaner, v8carlist);
				UpdateFitsToCar(hiHatBase, v8carlist);
				UpdateFitsToCar(hiHatFilter, v8carlist);
				UpdateFitsToCar(hiHatTop, v8carlist);

			}
			new Log(@"New Parts Car Lists Updated.
");

		}

		//UPDATES PART FITSTOCAR LIST TO LIST GIVEN
		public void UpdateFitsToCar(Part part, List<string> carList)
		{
			try
			{
				part.PartInfo.FitsToCar = carList.ToArray();
			}
			catch (Exception e)
			{
				new Log($@"

!!!{part}'s FitsToCar NOT UPDATED!!!
{e}

");
			}

		}

		//ADDS PARTS TO SURVIVAL SPAWN LIST. MIGHT BE UNNECESSARY WITH MODUTILS UPDATE
		public void AddToSurvival()
		{
			new Log("AddToSurvival Ran.");
			GameObject partParent = GameObject.Find("PartsParent");
			if (partParent?.GetComponent<JunkPartsList>())
			{
				JunkPartsList partslist = partParent.GetComponent<JunkPartsList>();
				GameObject[] parts = new GameObject[partslist.Parts.Length + newParts.Count];
				partslist.Parts.CopyTo(parts, 0);
				newParts.CopyTo(parts, partslist.Parts.Length);
				partslist.Parts = parts;
				new Log("Items Added to Survival.");
				addedToSurvival = true;
			}
		}

		//THIS IS A WHOLE CAN OF WORMS I'M AFRAID TO TOUCH RN
		public void AddToCars()
		{

		}

		//SOON... MAYBE
		public void AddToPerformanceShop()
		{

		}

		//UNNECESSARY RN
		public void AddToInteriorShop()
		{

		}

		//VARIABLES
		//SETTINGS
		public static Checkbox verboseLogging;
		//LISTS
		public List<GameObject> catalogueParts = new List<GameObject>() { };
		private readonly List<string> originalName = new List<string> { };
		private readonly List<GameObject> originalParts = new List<GameObject> { };
		private readonly List<GameObject> newParts = new List<GameObject> { };
		//PARTS
		//PREMADE
		public Part hiHatTop;
		public Part hiHatFilter;
		public Part hiHatBase;
		public Part pipeAirCleaner;
		public Part v8FanExtension;
		public Part filterExtension1;
		public Part intakei6Perf;
		public Part blowerCrankPulleyi6;
		public Part tunnelRamv8;
		public Part squishFilter;
		public Part squishCleaner;
		public Part triplePortScoopFilter;
		public Part triplePortScoop;
		public Part blowerBeltDoublei6;
		public Part blowerBeltSinglev8Perf;
		public Part blowerBeltSinglev8;
		public Part blowerBeltSinglei6;
		public Part singleCarbBlower;
		//PREFAB
		public Part bikeShockFLLong;
		public Part bikeShockFRLong;
		public Part bikeTire21;
		public Part bikeTire21_L;
		public Part spoke5BikeRim21Rear;
		public Part spoke5BikeRim21Front;
		public Part spoke5BikeRim19Rear;
		public Part spoke5BikeRim19Front;
		public Part bikeRim21Back;
		public Part bikeRim21Front;
		//INTERNAL
		public Part headcover0AChrome;
		public Part headcover00Chrome;
		public Part sparkwires0AHP;
		public Part rockers0AHP;
		public Part head20AHP;
		public Part head10AHP;
		public Part cylinder20AHP;
		public Part cylinder10AHP;
		public Part crankshaft0AHP;
		public Part carburetor0AHP;
		public Part camshaft0AHP;
		public Part sparkwires00HP;
		public Part head00HP;
		public Part cylinder00HP;
		public Part crankshaft00HP;
		public Part carburetor00HP;
		public Part camshaft00HP;
		public Part sparkplugsHP;
		public Part coil06HP;
		public Part head06HP;
		public Part clutch06HP;
		public Part flywheel06LW;
		public Part head12HP;
		public Part airfilter12HP;
		public Part glowplugrelay12HP;
		public Part glowplugHP;
		public Part blocki6d34;
		public Part diff0660;
		public Part diff0650;
		public Part diff0628;
		public Part diff0624;
		public Part diff0620;
		public Part diff0615;
		public Part diff0760;
		public Part diff0750;
		public Part diff0728;
		public Part diff0724;
		public Part diff0720;
		public Part diff0715;
		public Part diff1260;
		public Part diff1250;
		public Part diff1228;
		public Part diff1224;
		public Part diff1220;
		public Part diff1215;
		public Part gearbox00six;
		public Part gearbox12six;
		public Part gearbox07six;
		public Part gearbox06six;
		public Part batteryHP;
		public Part radiator12LW;
		public Part radiator06LW;
		public Part radiator07LW;
		public Part harmonicbalancer07LW;
		public Part clutch07HP;
		public Part fuelpump07HP;
		public Part flywheel07LW;
		public Part coil07HP;
		public Part blockv872;
		//MATERIALS
		public Shader chromeShader;
		public Material chrome;
		public Material chromeRed;
		public Material darkSteel;
		public Material metallicOrange;
		public Material metallicRed;
		public Material yellow;
		//OTHER
		public bool isSurvival;
		public static bool addedToSurvival = false;
		public static bool partsBuilt = false;
		public GameObject sparkplugHP;
		public SHOPitem box;
		private Texture2D thumbnail;
		private Texture2D[] thumbnails;
		private AssetBundle bundle;
		public ModInstance ThisMod;
	}

	//BOOST MAX RPM TO 7000
	public class RPMBoost : MonoBehaviour
	{
		//
		public void Start()
		{
			ModUtils.PlayerCarChanged += CarChanged;
			new Log("Flywheel Started.");
		}

		//
		public void CarChanged()
		{
			new LogV("Car Changed.");
			if (transform.parent)
			{
				car = transform.root.GetComponent<MainCarProperties>();
				if (car && car.SittingInCar)
				{
					new Log("Sat In FlyWheel Affected Car!!!");
					con = transform.root.GetComponent<VehicleController>();
					if (con)
					{
						new LogV("Vehicle Controller Found.");
						eng = con.powertrain.engine;
						if (eng != null)
						{
							new LogV("Engine Found.");
							//eng.idleRPM = 1000;
							eng.maxRPM = 7000;
							eng.revLimiterRPM = 7000;
						}
						else
							new LogV("Engine NOT Found!!!");
					}
					else
						new LogV("Vehicle Controller NOT Found!!!");
					wheels = car.gameObject.GetComponentsInChildren<WheelController>();
					if (wheels.Length > 0)
					{
						new LogV($"{wheels.Length} Wheels Found.");
						foreach (WheelController wheel in wheels)
						{
							wheel.MaxRPM = 7000;
							new LogV("Wheel Updated.");
						}
					}
					else
						new LogV("Wheels NOT Found!!!");
					new Log();
				}
				else
				{
					car = null;
					con = null;
					eng = null;
					wheels = null;
					new LogV(@"FlyWheel Not Attached to Current Car.
");
				}
			}
			else
				new LogV(@"FlyWheel Not Attached to Anything.
");
		}
			
		//
		public void OnDestroy()
		{
			ModUtils.PlayerCarChanged -= CarChanged;
		}

		private WheelController[] wheels;
		private EngineComponent eng;
		private VehicleController con;
		private MainCarProperties car;
	}

	//LENGTHEN BIKE FRONT SUSPENSION
	public class SpringExtension : MonoBehaviour
	{
		//
		public void Start()
		{
			ModUtils.PlayerCarChanged += CarChange;
			new Log("Bike Spring Started.");
			if (transform.parent)
			{
				StartCoroutine(startUpDelay());
			}
		}

		//
		IEnumerator startUpDelay()
		{
			yield return new WaitForSeconds(5);
			CarChange();
			startup = false;
		}

		//
		public void CarChange()
		{
			car = transform.root.GetComponent<MainCarProperties>();
			if (car && car.SittingInCar || startup)
			{
				new Log("Bike Spring Affected Bike");
				wheels = car.gameObject.GetComponentsInChildren<WheelController>();
				if (wheels.Length > 0)
				{
					new LogV($"{wheels.Length} Wheels Found.");
					foreach (WheelController wheel in wheels)
					{
						if (wheel.gameObject.name == "WheelControllerFront")
						{
							wheel.suspensionDistance = 0.5f;
							wheel.springLength = 0.5f;
							new LogV("Wheel Updated.");
						}
					}
				}
				else
					new LogV("Wheels NOT Found!!!");
				new Log();
			}
			else
			{
				car = null;
				wheels = null;
				new LogV(@"Bike Spring Not Attached to Current Car.
");
			}
		}

		//
		public void OnDestroy()
		{
			ModUtils.PlayerCarChanged -= CarChange;
		}

		MainCarProperties car;
		WheelController[] wheels;
		private bool startup = true;
	}

	//MAKE BIKE HEADLIGHT UNBREAKABLE
	public class UnbreakableLight : MonoBehaviour
	{
		public void Start()
		{
			ModUtils.PlayerCarChanged += CarChange;
			new Log("Bike Headlight Started.");
			CarChange();
			
		}

		public void CarChange()
		{
			shatter = transform.GetComponentInChildren<RVP.ShatterPart>();
			if (shatter != null)
			{
				shatter.enabled = false;
				new Log(@"Bike Headlight Fixed.
");
			}
		}

		public void OnDestroy()
		{
			ModUtils.PlayerCarChanged -= CarChange;
		}

		private ShatterPart shatter;
	}

	//COPYPASTA
	/*public void Start()
	{
		ModUtils.PlayerCarChanged += CarChange;
	}

	public void CarChange()
	{

	}

	public void OnDestroy()
	{
		ModUtils.PlayerCarChanged -= CarChange;
	}*/
}