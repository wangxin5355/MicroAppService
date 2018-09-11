﻿using RestTemplateCore;
using System;
using System.Net.Http;

namespace consultest1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                RestTemplate rest = new RestTemplate(httpClient);

                Console.WriteLine("---查询数据---------");
                var ret1 = rest.GetForEntityAsync<Product[]>("http://ProductService/api/Product/").Result;
                Console.WriteLine(ret1.StatusCode);
                if (ret1.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    foreach (var p in ret1.Body)
                    {
                        Console.WriteLine($"id={p.Id},name={p.Name}");
                    }
                }

                Console.WriteLine("---新增数据---------");
                Product newP = new Product();
                newP.Id = 888;
                newP.Name = "辛增";
                newP.Price = 88.8;
                var ret = rest.PostAsync("http://ProductService/api/Product/", newP).Result;
                Console.WriteLine(ret.StatusCode);
            }
            Console.ReadKey();
        }

        private class Product
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public double Price { get; set; }
            public string Description { get; set; }
        }
    }
}