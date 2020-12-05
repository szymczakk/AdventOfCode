using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace d4
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public UnitTest1(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        
        [Fact]
        public void Test1()
        {
        const string seed = @"ecl:gry pid:860033327 eyr:2020 hcl:#fffffd
byr:1937 iyr:2017 cid:147 hgt:183cm

iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884
hcl:#cfa07d byr:1929

hcl:#ae17e1 iyr:2013
eyr:2024
ecl:brn pid:760753108 byr:1931
hgt:179cm

hcl:#cfa07d eyr:2025 pid:166559648
iyr:2011 ecl:brn hgt:59in";
            var result = D.T1(seed);
            
            Assert.Equal(2, result);
        }

        [Fact]
        public async Task Prod1()
        {
            var input = await D.LoadFile();
            var result = D.T1(input);
            
            _testOutputHelper.WriteLine(result.ToString());
            
            Assert.Equal(242, result);
        }

        [Fact]
        public void Test2()
        {
            var validPassportsSeed = @"pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980
hcl:#623a2f

eyr:2029 ecl:blu cid:129 byr:1989
iyr:2014 pid:896056539 hcl:#a97842 hgt:165cm

hcl:#888785
hgt:164cm byr:2001 iyr:2015 cid:88
pid:545766238 ecl:hzl
eyr:2022

iyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719";

            var invalidPasportsSeed = @"eyr:1972 cid:100
hcl:#18171d ecl:amb hgt:170 pid:186cm iyr:2018 byr:1926

iyr:2019
hcl:#602927 eyr:1967 hgt:170cm
ecl:grn pid:012533040 byr:1946

hcl:dab227 iyr:2012
ecl:brn hgt:182cm pid:021572410 eyr:2020 byr:1992 cid:277

hgt:59cm ecl:zzz
eyr:2038 hcl:74454a iyr:2023
pid:3556412378 byr:2007";
            
            var validPassportsResult = D.T2(validPassportsSeed);
            Assert.Equal(4, validPassportsResult);

            var invalidPasportsResutl = D.T2(invalidPasportsSeed);
            Assert.Equal(0, invalidPasportsResutl);
        }

        [Fact]
        public async Task Prod2()
        {
            var input = await D.LoadFile();
            var result = D.T2(input);
            
            _testOutputHelper.WriteLine(result.ToString());
            
            Assert.Equal(186, result);
        }
        
    }

    public static class D
    {
        public static async Task<string> LoadFile()
        {
            return await System.IO.File.ReadAllTextAsync("./input.txt");
        }
        
        private static List<(string name, bool isRequired, Func<string, bool> valid)> rules = new List<(string name, bool isRequired, Func<string, bool>)>()
        {
            ("byr", true, IsByrValid),
            ("iyr", true, IsIyrValid),
            ("eyr", true, IsEyrValid),
            ("hgt", true, IsHgtValid),
            ("hcl", true, IsHclValid),
            ("ecl", true, IsEclValid),
            ("pid", true, IsPidValid)
        };

        private static bool IsByrValid(string value)
        {
            var parsable = int.TryParse(value, out var q);

            return parsable && q >= 1920 && q <= 2002;
        }

        private static bool IsIyrValid(string value)
        {
            var parsable = int.TryParse(value, out var q);

            return parsable && q >= 2010 && q <= 2020;
        }
        private static bool IsEyrValid(string value)
        {
            var parsable = int.TryParse(value, out var q);

            return parsable && q >= 2020 && q <= 2030;
        }
        private static bool IsHgtValid(string value)
        {
            var validUnits = new string[] {"cm", "in"};

            if (!validUnits.Any(value.Contains))
            {
                return false;
            }

            if (value.IndexOf("cm", StringComparison.InvariantCultureIgnoreCase) != -1)
            {
                var parsable = int.TryParse(value.Substring(0, 3), out var cmValue);
                return parsable && cmValue >= 150 && cmValue <= 193;
            }
            else
            {
                var parsable = int.TryParse(value.Substring(0, 2), out var inValue);
                return parsable&& inValue >= 59 && inValue <= 76;
            }
        }
        
        private static bool IsHclValid(string value)
        {
            const string regex = "^#([a-fA-F0-9]{6})";
            return Regex.IsMatch(value, regex);
        }
        
        private static bool IsEclValid(string value)
        {
            var valid = new string[] {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};
            return valid.Contains(value);
        }
        private static bool IsPidValid(string value)
        {
            return value.Length == 9 && int.TryParse(value, out var _);
        }
        

        private static Dictionary<string, string> ParsePassport(string passport)
        {
            var props = passport.Trim().Split(" ");
            var result = new Dictionary<string, string>();
            
            foreach (var prop in props)
            {
                var q = prop.Split(":");
                result.Add(q[0], q[1]);
            }

            return result;
        }

        private static IEnumerable<string> GetPasswordString(string input)
        {
            var lines = input.Split(Environment.NewLine);
            var pasports = new List<string>();

            var sb = new StringBuilder();

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    pasports.Add(sb.ToString());
                    sb = new StringBuilder();
                }

                sb.AppendFormat("{0} ", line);
            }
            
            pasports.Add(sb.ToString());

            return pasports;
        }

        private static bool IsPasswordValid(string passport)
        {
            var valid = false;
            foreach (var rule in rules)
            {
                if (rule.isRequired)
                {
                    if (passport.IndexOf(rule.name, StringComparison.InvariantCultureIgnoreCase) == -1)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static bool IsPasswordValidAvanced(Dictionary<string, string> passport)
        {
            foreach (var rule in rules)
            {
                if (!passport.ContainsKey(rule.name))
                {
                    return false;
                }

                if (!rule.valid(passport[rule.name]))
                {
                    return false;
                }
            }

            return true;
        }
        
        public static int T1(string arg)
        {
            var passports = GetPasswordString(arg);

            var validCount = 0;
            
            foreach (var passport in passports)
            {
                if (IsPasswordValid(passport))
                {
                    validCount++;
                }
            }
            
            return validCount;
        }

        public static int T2(string arg)
        {
            var passports = GetPasswordString(arg).Select(ParsePassport);

            return passports.Count(IsPasswordValidAvanced);
        }
    }
}