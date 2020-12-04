using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace d4
{
    public class UnitTest1
    {
        private const string seed = @"ecl:gry pid:860033327 eyr:2020 hcl:#fffffd
byr:1937 iyr:2017 cid:147 hgt:183cm

iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884
hcl:#cfa07d byr:1929

hcl:#ae17e1 iyr:2013
eyr:2024
ecl:brn pid:760753108 byr:1931
hgt:179cm

hcl:#cfa07d eyr:2025 pid:166559648
iyr:2011 ecl:brn hgt:59in";
        
        [Fact]
        public async Task Test1()
        {
            var result = D.T1(seed);
            
            Assert.Equal(2, await result);
        }
    }

    public static class D
    {
        public static async Task<string> LoadFile()
        {
            return await System.IO.File.ReadAllTextAsync("./input.txt");
        }
        
        private static List<(string name, bool isRequired)> rules = new List<(string name, bool isRequired)>()
        {
            ("byr", true),
            ("iyr", true),
            ("eyr", true),
            ("hgt", true),
            ("hcl", true),
            ("ecl", true),
            ("pid", true),
            ("cid", false)
        };

        private static (string byr, string iyr, string eyr, string hgt, string hcl, string ecl, string pid, string cid) ParsePassport(string p)
        {
            var props = p.Split(" ");

            string byr = null;
            string iyr = null;
            string eyr = null;
            string hgt = null;
            string hcl = null;
            string ecl = null;
            string pid = null;
            string cid = null;
            
            foreach (var prop in props)
            {
                var q = prop.Split(":");
                
            }

            return (byr, iyr, eyr, hgt, hcl, ecl, pid, cid);
        }
        
        public static Task<int> T1(string arg)
        {
            var passports = arg.Split(Environment.NewLine).Select(ParsePassport);

            return Task.FromResult(0);
        }
    }
}