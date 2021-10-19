﻿using System;

using Psim.Particles;
using Psim.ModelComponents;
using Psim.Materials;

namespace Psim
{
	class Program
	{
		static void Main(string[] args)
		{
			DispersionData dData;
			dData.LaData = new double[] { -2.22e-7, 9260.0, 0.0};
			dData.TaData = new double[] { -2.28e-7, 5240.0, 0.0};
			dData.WMaxLa = 7.63916048e13;
			dData.WMaxTa = 3.0100793072e13;

			RelaxationData rData;
			rData.Bl = 1.3e-24;
			rData.Btn = 9e-13;
			rData.Btu = 1.9e-18;
			rData.BI = 1.2e-45;
			rData.W = 2.42e13;

			Material silicon = new Material(in dData, in rData);

			Sensor s1 = new Sensor(1, silicon, 300);
			Cell c1 = new Cell(10, 10, s1);
			Sensor s2 = new Sensor(2, silicon, 300);
			Cell c2 = new Cell(1, 1, s2);



			// Test transition surface (HandlePhonon)
			Console.WriteLine("\t\t***Test trasition surface***");
			Phonon p1 = new Phonon(1);
			double px = 10;
			p1.SetCoords(px, 1);
			p1.SetDirection(0.5, 0.5);
			Console.WriteLine($"position before {px}");
			TransitionSurface ts = new TransitionSurface(SurfaceLocation.right, c2);
			Cell c3 = ts.HandlePhonon(p1);
			p1.GetCoords(out px, out double py);
			Console.WriteLine($"position After {px}");
			Console.WriteLine($"New cell {c3}");


			// Test emit surface (HandlePhonon)
			Console.WriteLine("\t\t***Test Emit surface***");
			Phonon p2 = new Phonon(1);
			p2.DriftTime = 10;
			Console.WriteLine("Phonon properties prior to emit surface collision");
			Console.WriteLine($"Active: {p2.Active}");
			Console.WriteLine($"Drift Time: {p2.DriftTime}");
			EmitSurface es = new EmitSurface(SurfaceLocation.left, c1, 300);
			es.HandlePhonon(p2);
			Console.WriteLine("Phonon properties after to emit surface collision");
			Console.WriteLine(p2.Active);
			Console.WriteLine(p2.DriftTime);


		}
	}
}
