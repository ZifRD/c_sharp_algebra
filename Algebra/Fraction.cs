using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Algebra
{
    [Serializable]
    public class Fraction
    {
        private int num;
        private int den;

        private void UsePrimesSimplify(int number,int min)
        {
            string filename = "";
            switch (number)
            {
                case 120: filename = "2_120.prm"; break;
                case 720: filename = "121_720.prm"; break;
                case 5040: filename = "721_5040.prm"; break;
            }
            FileStream v = File.OpenRead(filename);
            BinaryFormatter b = new BinaryFormatter();
            Primes smp = (Primes)b.Deserialize(v);
            v.Close();
            for (int i = 0; i < smp.Elems.Count; i++)
            {
                int y = (int)smp.Elems[i];
            py: if (min >= y && num % y == 0 && den % y == 0)
                {
                    num /= y;
                    den /= y;
                    min /= y;
                    goto py;
                }
                if (min < y) break;
            }
        }

        public Fraction(int ch, int zn)
        {

            this.num = ch;
            this.den = zn;
            int min = 0;

            if (num % den == 0)
            {
                num /= den;
                den = 1;
                goto next;
            }

            int yy = num - den * (num / den);

            if (Math.Abs(yy) > Math.Abs(den)) min = Math.Abs(den);
            else min = Math.Abs(yy);

            if (min < 2) goto follow;

            if (den == num)
            {
                den = num = 1;
            }
            UsePrimesSimplify(120,min);
            if (min <= 120) goto next;
            UsePrimesSimplify(720,min);
            if (min <= 720) goto next;
            UsePrimesSimplify(5040,min);
            if (min <= 5040) goto next;
            //if (min <= 120)
            //{
            //    FileStream v = File.OpenRead("2_120.prm");
            //    BinaryFormatter b = new BinaryFormatter();
            //    Primes smp = (Primes)b.Deserialize(v);
            //    v.Close();
            //    for (int i = 0; i < smp.mem.Count; i++)
            //    {
            //        int y = (int)smp.mem[i];
            //    py: if (min >= y && num % y == 0 && den % y == 0)
            //        {
            //            num /= y;
            //            den /= y;
            //            min /= y;
            //            goto py;
            //        }
            //        if (min < y) break;
            //    }
            //    if (min <= 120) goto next;
            //}
            //if (min <= 720)
            //{
            //    FileStream v = File.OpenRead("121_720.prm");
            //    BinaryFormatter b = new BinaryFormatter();
            //    Primes smp = (Primes)b.Deserialize(v);
            //    v.Close();
            //    for (int i = 0; i < smp.mem.Count; i++)
            //    {
            //        int y = (int)smp.mem[i];
            //    py: if (min >= y && num % y == 0 && den % y == 0)
            //        {
            //            num /= y;
            //            den /= y;
            //            min /= y;
            //            goto py;
            //        }
            //        if (min < y) break;
            //    }
            //    if (min <= 720) goto next;
            //}
            //if (min <= 5040)
            //{
            //    FileStream v = File.OpenRead("721_5040.prm");
            //    BinaryFormatter b = new BinaryFormatter();
            //    Primes smp = (Primes)b.Deserialize(v);
            //    v.Close();
            //    for (int i = 0; i < smp.mem.Count; i++)
            //    {
            //        int y = (int)smp.mem[i];
            //    py: if (min >= y && num % y == 0 && den % y == 0)
            //        {
            //            num /= y;
            //            den /= y;
            //            min /= y;
            //            goto py;
            //        }
            //        if (min < y) break;
            //    }
            //    if (min <= 5040) goto next;
            //}
            

            next: if (den < 0)
            {
                den *= -1;
                num *= -1;
            }

            follow: if (num == 0)
            {
                den = 1;
            }

        }

        public Fraction()
        {
            this.num = 1;
            this.den = 1;
        }

        public int Num
        {
            get { return num; }
        }

        public int Den
        {
            get { return den; }
        }


        public static Fraction operator +(Fraction a, Fraction b)
        {
            return new Fraction(a.num * b.den + b.num * a.den, a.den * b.den);
        }
        public static Fraction operator -(Fraction a, Fraction b)
        {
            return new Fraction(a.num * b.den - b.num * a.den, a.den * b.den);
        }
        public static Fraction operator *(Fraction a, Fraction b)
        {
            return (a.num == 0) ? new Fraction(a.num, a.den) : new Fraction(a.num * b.num, a.den * b.den);
        }
        public static Fraction operator /(Fraction a, Fraction b)
        {
            return (a.num == 0) ? new Fraction(a.num, a.den) : new Fraction(a.num * b.den, a.den * b.num);
        }
        public static bool operator ==(Fraction a, Fraction b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Fraction a, Fraction b)
        {
            return (a.num != b.num || a.den != b.den) ? true : false;
        }
        public override string ToString()
        {
            string line = ((num >= 0) ? " " : "") + num.ToString() + '/' + den.ToString();
            return line;
        }
        public override bool Equals(object obj)
        {
            return (this.num == ((Fraction)obj).num && this.den == ((Fraction)obj).den) ? true : false;
        }
        public override int GetHashCode()
        {
            return num.GetHashCode();
        }
    }
}
