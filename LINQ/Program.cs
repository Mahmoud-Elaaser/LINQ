using LINQ.Data;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using static LINQ.ListGenerators;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
             
            #region 05 Linq Syntax
                Console.WriteLine("------------Linq Syntax------------");
                List<int> Numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                Console.WriteLine("--1. Fluent syntax--");
                #region 1. Fluent syntax
                //// 1.1=> call "Linq Operator" as an Extension method
                var oddNumbers = Numbers.Where(x => x % 2 == 1);
                foreach (var number in oddNumbers)
                {
                    Console.WriteLine(number);
                }

                //// 1.2=> call "Linq Operator" as a static method thraugh Enumerable class
                var oddNumbers2 = Enumerable.Where(Numbers, x => x % 2 == 1);
                foreach (var item in oddNumbers2)
                {
                    Console.WriteLine(item);
                }
                #endregion

                Console.WriteLine("--2. Query syntax or Query Expression--");
                #region 2. Query syntax or Query Expression
                var oddNumbers3 = from x in Numbers where x % 2 == 1 select x;
                foreach (var item in oddNumbers3)
                {
                    Console.WriteLine(item);
                }
                #endregion


            #endregion
            Console.WriteLine("*************************************");

            #region 06 Execution Ways [Deffered, Immediate]
                Console.WriteLine("--------Deffered Execution------");
                #region 1. Deffered Execution(latest version of data) 10 category are deffered and only 3 are Immediate
                List<int> Numbers2 = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                var oddNumbers4 = Numbers2.Where(x => x %2 == 1);
                Numbers2.AddRange(new int[] { 11, 12, 13, 14, 15 });
                foreach(var number in oddNumbers4) { Console.WriteLine(number);}
                #endregion

                Console.WriteLine("--------Immediate Execution------");
                #region 2. Immediate Execution[Elements Operators, Casting Operators, Aggregate Operators] three only
                List<int> Numbers3 = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                var oddNumbers5 = Numbers3.Where(x => x % 2 == 1).ToList();
                Numbers2.AddRange(new int[] { 11, 12, 13, 14, 15 });
                foreach (var number in oddNumbers5) { Console.WriteLine(number); }
                #endregion

            #endregion
            Console.WriteLine("*************************************");

            #region 07 Data Setup
            Console.WriteLine(ProductList[0]);
            Console.WriteLine(CustomerList[0]);
            #endregion
            Console.WriteLine("*************************************");

            #region 08 Filtration [Restriction] Operator - Where
                #region Products Out Of Stock
                ////Fluent syntax
                var Result = ProductList.Where(x => x.UnitsInStock == 0);
                foreach (var item in Result)
                {
                    Console.WriteLine(item);
                }

                Console.WriteLine("--------------");
                ////Query syntax
                var Result2 = from N in ProductList 
                              where (N.UnitsInStock == 0) 
                              select N;
                foreach (var item in Result2)
                {
                    Console.WriteLine(item);
                }
                #endregion
                Console.WriteLine("---------------------------");

                #region Products In Stock And Category Is Meat/Poultry
                var Result4 = ProductList.Where(x => x.UnitsInStock > 0 && x.Category == "Meat/Poultry"); //// fluent syntax
                foreach (var item in Result4)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("-------------");
                //// Query syntax
                var Result5 = from N in ProductList
                              where (N.UnitsInStock > 0 && N.Category == "Meat/Poultry") 
                              select N;
                foreach (var item in Result5)
                {
                    Console.WriteLine(item);
                }
                #endregion

                Console.WriteLine("---------------------------");
                //// Indexed where [valid in Fluent syntax only], cann't be written in Query syntax
                //// search in range of (0, i-1)
                var Result6 = ProductList.Where((x, i) => i < 10 && x.UnitsInStock == 0);
                foreach (var item in Result6)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("---------------------------");

            #endregion
            Console.WriteLine("*************************************");

            #region 09 Transformation [Projection] operators - Select, Select Many
            //// Fluent syntax
            var Result7 = ProductList.Select(x => x.ProductName);
            foreach (var item in Result7)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("--------------");

            //// Query Syntax
            var Result8 = from N in ProductList select N.ProductName;
            foreach (var item in Result8)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("-------------Select Many--------------");
            ////fluent syntax
            var Result9 = CustomerList.SelectMany(x => x.Orders);
            foreach (var item in Result9)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("------------");

            ////Query syntax
            var Result10 = from N in CustomerList 
                           from O in N.Orders 
                           select O; //// because it is a select many
            foreach (var item in Result10)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("-------------Select Two Properties------------");
            //// fluent syntax (by using Anonymous Object)
            var Result11 = ProductList.Select(x => new  
                           { 
                               ProductID = x.ProductID, ProductName = x.ProductName 
                           });
            foreach (var item in Result11)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-------------");

            //// Query syntax
            var Result12 = from N in ProductList
                            select new
                            {
                                ProductID = N.ProductID,
                                ProductName = N.ProductName
                            };
            Console.WriteLine("------Make a discount-------");
            //// fluent syntax
            var discountedProductlist = ProductList.Where(x => x.UnitsInStock > 0).Select(x => new
            {
                Id = x.ProductID,
                Name = x.ProductName,
                OldPrice = x.UnitPrice,
                NewPrice = x.UnitPrice - (x.UnitPrice * 0.1M)
            });
            foreach (var item in discountedProductlist)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-----------");
            var discountedProductlist2 = from N in ProductList
                                            where N.UnitsInStock > 0
                                            select new
                                            {
                                                Id =N.ProductID,
                                                Name = N.ProductName,
                                                OldPrice = N.UnitPrice,
                                                NewPrice = N.UnitPrice - (N.UnitPrice * 0.1M)
                                            };
            foreach (var item in discountedProductlist2)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("-------Idexed Select--------"); //// valid only with fluent syntax
            var Result13 = ProductList.Where(x => x.UnitsInStock > 0)
                                      .Select((x, i) => new {
                                        Index = i,
                                        ProductName = x.ProductName
                                      });
            foreach (var item in Result13)
            {
                Console.WriteLine(item);
            }
            #endregion
            Console.WriteLine("*************************************");

            #region 10 Ordering Operators
            Console.WriteLine("----------OrderBy [ascending order]-------");
            // fluent syntax
            var Result14 = ProductList
                           .OrderBy(x => x.UnitPrice); // ascending order
            foreach (var item in Result14)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("------Query syntax------");
            ////Query syntax
            var Result15 = from N in ProductList 
                           orderby N.UnitPrice 
                           select N;
            foreach (var item in Result15)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("----------OrderBy [Descending order]-------");
            var Result16 = ProductList
                           .OrderByDescending(x => x.UnitPrice); ////fluent syntax
            foreach (var item in Result16)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("----------");

            var Result17 = from N in ProductList 
                           orderby N.UnitPrice descending 
                           select N;                        ////Query syntax
            foreach (var item in Result17)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("----------ThenBy----------"); //// if two properties are equal
            var Result18 = ProductList.OrderBy(x => x.UnitPrice).ThenBy(x => x.UnitsInStock);
            foreach (var item in Result18)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-------");

            var Result19 = from N in ProductList orderby N.UnitPrice descending , N.UnitsInStock select N; ////Query syntax
            foreach (var item in Result19)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("----------Reverse---------");
            var Result20 = ProductList.Where(x => x.UnitsInStock == 0).Reverse();
            foreach (var item in Result20)
            {
                Console.WriteLine(item);
            }
            #endregion
            Console.WriteLine("*************************************");


            #region 11 Element Operators [Immediate Execution]
            //// Take care of Exception
            Console.WriteLine("-------Element Operators [Immediate Executio]------");
            
            Console.WriteLine("---First----"); //// if the collection is Empty it will throw Exception
            var Result21 = ProductList.First();
            Console.WriteLine(Result21);
            Console.WriteLine("--Overload of First--");
            var Result22 = ProductList.FirstOrDefault(x=>x.UnitsInStock==0); ////OverLoad of First 
            Console.WriteLine(Result22);
            
            Console.WriteLine("---Last---"); //// if the collection is Empty it will throw Exception
            var Result23 = ProductList.Last();
            Console.WriteLine(Result23?.ProductName??"Not Found"); //// ? => not null --- ?? => null
            Console.WriteLine("--Overload of Last--");
            var Result24 = ProductList.FirstOrDefault(x => x.UnitsInStock == 0); ////OverLoad of Last 
            Console.WriteLine(Result24);

            Console.WriteLine("---FirstOrDefault---"); //// if the collection is Empty ==> it will NOT throw Exception
            List<Product>TestList = new List<Product>();
            Console.WriteLine(TestList.FirstOrDefault());

            Console.WriteLine("------ElementAt-----");
            var Result25 = ProductList.ElementAt(10);  //// if the index doesnot exist ==> it will throw an Exception
            Console.WriteLine(Result25?.ProductName??"not found");
            
            var Result26 = ProductList.ElementAtOrDefault(1000); //// if the index doesnot exist ==> it will NOT throw an Exception
            Console.WriteLine(Result26);

            //Console.WriteLine("-----Single-------");
            //var Result27 = ProductList.Single(); // if sequence contains just ONLY ONE Element ==> it will Return this Element
            //Console.WriteLine(Result27);         // if sequence contains More Than Element or Empty ==> it will throw an Exception 
            Console.WriteLine("-------SingleOrDefault-------");
            //var Result28 = ProductList.SingleOrDefault();
            ////  comment   if sequence contains just ONLY ONE Element ==> it will Return this Element
            ////    ~      if sequence contains More Than Element ==> it will throw an Exception
            ////    ~     if sequence is Empty ==> it will Return null
            //Console.WriteLine(Result28?.ProductName ?? "not found");

            //var Result28 = ProductList.SingleOrDefault(x=>x.UnitsInStock==0);
            ////comment   if sequence contains just ONLY ONE Element that match condition ==> it will Return this Element
            ////  ~       if sequence contains More Than Element that match condition ==> it will throw an Exception
            ////  ~       if sequence contains NO Element that match condition ==> it will Return null
            //Console.WriteLine(Result28?.ProductName ?? "not found");

            // Hybrid syntax ==> Query syntax + Fluent syntax [mix] ==> (Query syntax).FluentSyntax
            Console.WriteLine("------Hybrid syntax ==> Query syntax + Fluent syntax------");
            var Result29 = (from N in ProductList
                           where N.UnitsInStock == 0
                           select new
                           {
                               N.ProductName,
                               N.ProductID,
                               N.UnitsInStock
                           }).FirstOrDefault();
            Console.WriteLine(Result29);
            #endregion
            Console.WriteLine("*************************************");


            #region 12 Aggregate Operators [Immediate Execution]
                Console.WriteLine("-------Aggregate Operators [Immediate Execution]------");
                #region 1. Count
                var Result30 = ProductList.Count; //// List Property
                var Result31 = ProductList.Count(x => x.UnitsInStock == 0); //// LINQ Operator => if u want to make a condition
                Console.WriteLine("--Count [LINQ Operator]--");
                Console.WriteLine($"count with condition is: {Result31}");
                Console.WriteLine("--Count [List Property]--");
                Console.WriteLine($"count is: {Result30}");
                #endregion
                #region 2. Max and Min
                //Console.WriteLine("--Max--");
                //var Result32 = ProductList.Max(); 
                //// it will throw an Exception (if you are using Max with no parameters just Max()) ==> At least one object must implement IComparable,
                //// To AVOID this Product MUST Implement IComparable Interface
                //Console.WriteLine(Result32);

                //Console.WriteLine("--Min--");
                var Result33 = ProductList.Min(x => x.ProductName.Length); // it works 
                Console.WriteLine($"Min ProductName Length is: {Result33}");

                //// Hybrid syntax (Mix between query syntax and Fluent syntax)
                var Result34 = (from N in ProductList
                                where N.ProductName.Length == 4
                                select N).FirstOrDefault();
                Console.WriteLine(Result34);
                #endregion
                #region 3. Sum and Average
                Console.WriteLine("---sum and Average---");
                var Result35 = ProductList.Sum(x => x.UnitPrice);
                Console.WriteLine($"sum is: {Result35}");
                var Result36 = ProductList.Average(x => x.UnitPrice);
                Console.WriteLine($"Average is: {Result36}");
                #endregion
                #region Aggregate
                string[] Names = {"Aya", "Omar", "Amr", "Mohamed" };
                var Result37 = Names.Aggregate((str01, str02)=> $"{str01} {str02}");
                Console.WriteLine(Result37);
                #endregion
            #endregion
            Console.WriteLine("*************************************");

            #region Casting Operator [Immediate Execution]
            //// casting IEnumerable to List, array or Dictionary
            List<Product> products = ProductList.Where(x=>x.UnitsInStock==0).ToList();

            Product[] arr = ProductList.Where(x=>x.UnitsInStock==0).ToArray();

            Dictionary<long, Product> dic = ProductList.Where(x => x.UnitsInStock == 0).ToDictionary(x => x.ProductID); ////Key value is Product

            Dictionary<long, string> dic2 = ProductList.Where(x => x.UnitsInStock == 0).ToDictionary(x => x.ProductID, x => x.ProductName); // Key and Value

            HashSet<Product> set = ProductList.Where(x => x.UnitsInStock == 0).ToHashSet();

            #endregion
            Console.WriteLine("*************************************");

            
            #region Generation Operators - [Deffered Execution]
            //// valid with Fluent syntax Only
            //// the Only way to call them is: static method from Enumerable class
            var res = Enumerable.Range(0, 100); //// start and count (0..99) = for(i=0; i<100; ++i)
            foreach (var it in res) Console.Write(it+" ");
            Console.WriteLine();

            var res2 = Enumerable.Repeat(2, 100);
            //// it return IEnumerable of 100 elements each element = 2
            //// ==> in C# int n = 100, x = 2; while (n > 0) { Console.Write(x + " "); n--; }
            foreach (int it in res2)Console.Write(it+" ");
            Console.WriteLine();

            var list = Enumerable.Empty<Product>().ToList();
            var list2 = new List<Product>(); 
            //// Both list and list2 will generate an empty list of product
            foreach (var it in list) Console.Write(it + " ");
            #endregion
            Console.WriteLine("*************************************");


            #region Set Operators [Defered Execution]
            // Set Operators ==> Union Family [Union, Union All, Except, Intersect]
            var seq1 = Enumerable.Range(0, 100); //// 0...99
            var seq2 = Enumerable.Range(50, 100); //// 50...149

            var union = seq1.Union(seq2).ToList(); //// Union => remove dublication (Union = Concat + Distinct)
            foreach (var it in union) Console.Write(it + " ");
            Console.WriteLine();

            var concatination = seq1.Concat(seq2).ToList(); //// Concat => ther is dublication
            foreach (var it in concatination) Console.Write(it + " "); //// 0..99 + 50..149
            Console.WriteLine();

            var distinct = concatination.Distinct(); //// Distinct => remove duplication
            foreach (var it in distinct) Console.Write(it + " ");
            Console.WriteLine();

            var intersect = seq1.Intersect(seq2).ToList(); //// 50..99 ==> exist in both seq1 and seq2
            foreach (var it in intersect) Console.Write(it + " ");
            Console.WriteLine();

            var except = seq1.Except(seq2); //// 0..49 ==> exist in seq1 and not exist in seq2
            foreach (var it in except) Console.Write(it + " ");
            Console.WriteLine();

            var except2 = seq2.Except(seq1); //// 100..149 ==> exist in seq2 and not exist in seq1
            foreach (var it in except2) Console.Write(it + " ");
            Console.WriteLine();
            #endregion
            Console.WriteLine("*************************************");

            #region Quantifier Operators - [Deffered Execution] - Return boolean [True or False]
            bool ok = ProductList.Any(x=>x.UnitsInStock==0); 
            //// if sequance contains at least one element that match condition return true; else return false;
            Console.WriteLine(ok);

            bool all = ProductList.All(x => x.UnitsInStock == 0);
            //// if all elements in sequence match condition return true; else return false;
            Console.WriteLine(all);

            var sequance1 = Enumerable.Range(0, 100).ToList();
            var sequance2 = Enumerable.Range(0, 100).ToList();
            bool sequanceEqual = sequance1.SequenceEqual(sequance2); 
            //// if all elements in sequnce1 equal all elements in sequence2 return true; else return false;
            Console.WriteLine(sequanceEqual);
            #endregion
            Console.WriteLine("*************************************");

            #region Zip Operator [Deferred Execution]
            string[] names = { "Mahmoud", "Ahmed", "Muhammed", "Moustafa" };
            int[] numbers = Enumerable.Range(1, 10).ToArray();
            char[] chars = { 'a', 'b', 'c', 'd'};

            var zip = names.Zip(numbers);
            foreach (var it in zip) Console.WriteLine(it);
            //// output
            ////(Mahmoud, 1)
            ////(Ahmed, 2)
            ////(Muhammed, 3)
            ////(Moustafa, 4)
            Console.WriteLine();

            var zip2 = names.Zip(numbers, (s, num) => new {index = num, Name = s}); // Anonymous Object Just to Handle output
            foreach (var it in zip2) Console.WriteLine(it);
            //// output
            ////{ index = 1, Name = Mahmoud }
            ////{ index = 2, Name = Ahmed }
            ////{ index = 3, Name = Muhammed }
            ////{ index = 4, Name = Moustafa }
            Console.WriteLine();

            var zip3 = names.Zip(numbers, (x, n) => new { Number = n, Name = x });
            foreach (var it in zip3) Console.WriteLine(it);
            //// output
            ////{ Number = 1, Name = Mahmoud }
            ////{ Number = 2, Name = Ahmed }
            ////{ Number = 3, Name = Muhammed }
            ////{ Number = 4, Name = Moustafa }
            Console.WriteLine();

            var zip4 = chars.Zip(names, (x, n) => new {Char = x,  Name = n});
            foreach (var it in zip4) Console.WriteLine(it);
            //// output
            ////{ Char = a, Name = Mahmoud }
            ////{ Char = b, Name = Ahmed }
            ////{ Char = c, Name = Muhammed }
            ////{ Char = d, Name = Moustafa }
            Console.WriteLine();

            var complexZip = names.Zip(numbers, chars);
            foreach (var it in complexZip) Console.WriteLine(it);
            //// output
            ////(Mahmoud, 1, a)
            ////(Ahmed, 2, b)
            ////(Muhammed, 3, c)
            ////(Moustafa, 4, d)
            Console.WriteLine();

            #endregion
            Console.WriteLine("*************************************");


            #region Grouping Operators [Deffered Execution]

            #region Example01 Group Products based on Category
            //// Query syntax
            var Group = from N in ProductList
                    group N by N.Category;
            foreach(var it in Group)
            {
                Console.WriteLine(it.Key);
                foreach(var pro in it)
                {
                    Console.WriteLine($"                            {pro.ProductName}");
                }
            }
            Console.WriteLine("----------------------------------------------------------");
            //// Fluent syntax
            var Group2 = ProductList.GroupBy(x => x.Category);
            foreach (var it in Group2)
            {
                Console.WriteLine(it.Key);
                foreach (var pro in it)
                {
                    Console.WriteLine($"                            {pro.ProductName}");
                }
            }
            #endregion
            Console.WriteLine("-----------------------------------------------");

            #region Example02 Get Products in stock and Group by Categories [Category must contain more than 10 products]
            //// Query syntax
            var Group03 = from N in ProductList
                          where N.UnitsInStock != 0
                          group N by N.Category
                          into Category
                          where Category.Count() > 10
                          select new
                          {
                              CategoryName = Category.Key,
                              CountOfProduct = Category.Count()
                          };
            foreach(var it in Group03)
            {
                Console.WriteLine(it);
            }
            Console.WriteLine();

            //// fluent syntax
            var Group04 = ProductList.Where(x => x.UnitsInStock != 0)
                                     .GroupBy(x => x.Category)
                                     .Where(x => x.Count() > 10)
                                     .Select(x => new
                                     {
                                         CategoryName = x.Key,
                                         CountOfProduct = x.Count()
                                     });
            foreach (var it in Group04)
            {
                Console.WriteLine(it);
            }
            Console.WriteLine();

            #endregion
            #endregion
            Console.WriteLine("*************************************");


            #region Partitionin Operartors [Deffered Execution]
            //// Take, Take while, Skip, Skip Last, Skip while
            var take = ProductList.Where(x => x.UnitsInStock != 0).Take(5);
            foreach (var it in take)
            {
                Console.WriteLine(it);
            }
            Console.WriteLine("--------------------");
            var takeLast = ProductList.Where(x => x.UnitsInStock != 0).TakeLast(5);
            foreach (var it in takeLast)
            {
                Console.WriteLine(it);
            }
            Console.WriteLine("--------------------");

            var skip = ProductList.Where(x => x.UnitsInStock != 0).Skip(5);
            foreach (var it in skip) //// start from ProductID:7
            {
                Console.WriteLine(it);
            }
            Console.WriteLine("--------------------");
            var skipLast = ProductList.Where(x => x.UnitsInStock != 0).SkipLast(5);
            foreach (var it in skipLast) //// end with ProductID:72
            {
                Console.WriteLine(it);
            }

            int[] array = { 4, 3, 5, 6, 2, 1, 8, 9, 3, 4 };

            var check = array.TakeWhile((Num, i) => Num > i); //// works as if(Num < index) break;
            foreach (var it in check) //// 4 3 5 6
            {
                Console.Write(it + " ");
            }

            var skipWhile = array.SkipWhile(x => x % 6 != 0); 
            //// skip while condition is true;
            //// if (x % 6 == 0) take from this number to end; else skip  ==> just to understand easilly
            foreach (var it in skipWhile) Console.Write(it + " "); // 6 2 1 8 9 3 4
            #endregion
            Console.WriteLine("*************************************");


            #region Let and Into [valid with Query syntax only]
            List<string> lst = new List<string>(){ "Omar", "Ali", "Sally", "Mohamed", "Ahmed" };
            //// if(lst[i] == 'A' or 'O', 'U', 'I', 'E') continue;
            //// into => Restart Query with introducing a new Range
            var query = from N in lst
                        select Regex.Replace(N, "[AOUIEaouie]", "")
                        into NoVewelNames   // into => Restart Query with introducing a new Range
                        where NoVewelNames.Length > 3
                        select NoVewelNames;
            foreach (var it in query) Console.Write(it + " "); // Slly  Mhmd
            Console.WriteLine();

            //// let => continue query with adding new range in a new variable 
            var query2 = from N in lst
                         let NoVewelNames = Regex.Replace(N, "[AOUIEaouie]", "")
                         where (NoVewelNames.Length > 3)
                         select NoVewelNames;
            foreach (var it in query2) Console.Write(it + " "); // Slly Mhmd
            #endregion
        }
    }
}
