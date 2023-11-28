using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classlab1
{
    class Program
    {

        public class StrArray 
        {
            public string[] data_array { get; set; }

            public StrArray(string[] arr) 
            {
                data_array = new string[arr.Length];
                Array.Copy(arr,data_array,arr.Length);
            }

            public string new_string(int num) 
            {

                float[] buf1 = new float[0]; string str=null;

                str_to_float(data_array[num], ref buf1);

                float_to_str(out str, buf1);

                return str;

            }

            void float_to_str(out string str, in float[] fl) 
            {
                char[] buf3 = new char[0], bufelem = new char[0]; int ctr = 0;

                foreach (float elem in fl) 
                {

                    bufelem = elem.ToString().ToCharArray();
                    Array.Resize(ref buf3, buf3.Length + bufelem.Length+1);
                    for (int j = ctr; j < ctr + bufelem.Length; j++) 
                    {
                        buf3[j] = bufelem[j-ctr]; 
                        
                    }
                    buf3[buf3.Length - 1] = ' ';
                    ctr += bufelem.Length+1;

                }

                str = new string(buf3);

            }

            public float min_inStr(int num) //min,max,sum search
            {
                float[] buf1=new float[0];

                str_to_float(data_array[num],ref buf1);

                float min = float.MaxValue;
                foreach (float elem in buf1) { if (elem < min) min = elem; }
                return min;
            }

            public float max_inStr(int num) //min,max,sum search
            {
                float[] buf1 = new float[0];

                str_to_float(data_array[num], ref buf1);

                float max = float.MinValue;
                foreach (float elem in buf1) { if (elem > max) max = elem; }
                return max;
            }

            public float sum_inStr(int num) //min,max,sum search
            {
                float[] buf1 = new float[0];

                str_to_float(data_array[num], ref buf1);

                float sum = 0;
                foreach (float elem in buf1) { sum += elem; }
                return sum;
            }

            void str_to_float(in string str, ref float[] fl) 
            {
                char[] buf3 = new char[0]; 
                bool isNum = false, next=false;//num or blank
                bool intpart = false;

                for (int i=0;i<str.Length;i++) 
                {
                    if (str[i] == ' '||next) 
                    {
                        if (str[i] == ' ' && next == true) next = false;
                            isNum = false;
                        if(!noDuplic(buf3)) buf3 = new char[1] { '0' };
                        if (buf3.Length > 0) { toNumber(buf3, ref fl); buf3 = new char[0]; }
                    }
                    else
                    {
                        if (isNum == false) buf3 = new char[0];

                        isNum = true;

                        bool isdigit; isDigit(str[i], out isdigit, ref intpart);
                        if (intpart == false) isdigit = false;

                        if (isdigit) { Array.Resize(ref buf3,buf3.Length+1); buf3[buf3.Length-1]=str[i]; }
                        else { buf3 = new char[1] { '0' }; next=true; isNum = false; }

                        if (i == str.Length - 1) {
                            if (!noDuplic(buf3)) buf3 = new char[1] { '0' };
                            if (buf3.Length > 0) { toNumber(buf3, ref fl); buf3 = new char[0]; }
                        }
                    }
                }
            }

            void isDigit(char sym, out bool isdigit, ref bool intpart)     //out
            {
                isdigit = false; 
                for (int i = 0; i < 10; i++) 
                {
                    if (sym == '0' + i || sym ==','||sym=='-') isdigit = true;
                    if (sym == '0' + i) intpart = true;

                }
            }

            bool noDuplic(in char[] syms ) 
            {
                bool isit = true;
                int zapcount = 0, mincount = 0;

                foreach(char elem in syms) 
                {
                    if (elem == ',') zapcount++;
                    if (elem == '-') mincount++;
                    if (zapcount > 1 || mincount > 1) isit = false;
                }

                return isit;
            }

            void toNumber(char[] syms, ref float[] nums)     
            {
                string str = new string( syms);
                Array.Resize(ref nums,nums.Length+1); nums[nums.Length-1]=float.Parse(str);
                
            }

        }

        static void Main(string[] args)
        {
            //            Дан с клавиатуры двумерный массив значений из n строк.Значения вводятся
            //через пробел для каждой строки массива. Между значениями могут быть лишние
            //пробелы, а значения не всегда могут быть числовыми, все значения которые невозможно
            //конвертировать в число необходимо заменить нулем.Реализовать методы для поиска
            //минимального, максимального и суммы каждой строки массива. Крайне важно —
            //программа не должна критически завершаться! При реализации методов необходимо
            //использовать модификаторы ref, in, out.

            int n; string[] arr;

            Console.Write("Введите количество строк в массиве:  ");
            n = int.Parse( Console.ReadLine());
            arr = new string[n];

            Console.WriteLine("Введите строковый массив значений:  ");

            for (int i = 0; i < n; i++) 
            {
                arr[i] = Console.ReadLine();
            }

            StrArray stArr = new StrArray(arr);

            Console.WriteLine("----------------------------------------------  ");

            Console.WriteLine("Номер строки - числовое представление - min - max - sum  ");

            for (int i=0;i<n;i++) 
            {
                Console.WriteLine("     {0} - {1} - {2} - {3} - {4}  ",i+1, stArr.new_string(i),stArr.min_inStr(i),stArr.max_inStr(i),stArr.sum_inStr(i));
            }

        }
    }
}   
