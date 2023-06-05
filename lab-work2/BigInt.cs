namespace lab_work2;

class BigInt
{
    public char Sign; // +, -
    public List<int> Number;
    public static BigInt Zero => new BigInt();
    public static BigInt One => new BigInt("1");

    public BigInt(char sign, List<int> number)
    {
        Sign = sign;
        Number = number;
        if (Number.Count > 1)
            this.DeleteNuls();
    }

    public BigInt()
    {
        Sign = '+';
        Number = new List<int>() { 0 };
    }

    public BigInt(string str)
    {
        Number = new List<int>();
        if (str[0] == '+')
        {
            Sign = '+';
            str = str.Remove(0, 1);
        }
        else if (str[0] == '-')
        {
            Sign = '-';
            str = str.Remove(0, 1);
        }
        else if (char.IsDigit(str[0]))
        {
            Sign = '+';
        }
        else
            throw new ArgumentException();
        for (int i = str.Length - 1; i >= 0; i--)
            Number.Add(int.Parse(str[i].ToString()));
        this.DeleteNuls();
    }

    private void ChangeSign()
    {
        Sign = Sign == '+' ? '-' : '+';
    }

    private BigInt ChangedSign()
    {
        return new BigInt(Sign == '+' ? '-' : '+', Number);
    }

    public override string ToString()
    {
        var n = "";
        for (var i = Number.Count - 1; i >= 0; i--)
            n += Number[i].ToString();
        return Sign + n;
    }

    private void DeleteNuls()
    {
        while (Number.Count > 1 && Number[^1] == 0)
                Number.RemoveAt(Number.Count - 1);
    }

    private void SetDigit(int index, int val)
    {
        while (Number.Count <= index)
        {
            Number.Add(0);
        }

        Number[index] = val;
    }

    private void EqualizeTheDigits(int a)
    {
        if (a >= Number.Count)
            Number.AddRange(new List<int>(new int[a - Number.Count]));
    }

    private static BigInt Addition(BigInt a, BigInt b) //суммирует два бигинта
    {
        var res = new BigInt();
        var maxLen = Math.Max(a.Number.Count, b.Number.Count) + 1;
        var enlarge = 0;
        var num1 = a.Number; var num2 = b.Number; var resNum = res.Number;
        if (num1.Count < num2.Count) 
        { 
            a.EqualizeTheDigits(b.Number.Count + 1);
            b.EqualizeTheDigits(b.Number.Count + 1);
            res.EqualizeTheDigits(b.Number.Count + 1);
        }
        else 
        { 
            b.EqualizeTheDigits(a.Number.Count + 1); 
            a.EqualizeTheDigits(a.Number.Count + 1);
            res.EqualizeTheDigits(a.Number.Count + 1);
        }
        for (var i = 0; i < maxLen; i++)
        {
            var s = num1[i] + num2[i] + enlarge;
            if (s >= 10)
            {
                enlarge = 1;
                resNum[i] = s - 10;
            }
            else
            {
                enlarge = 0;
                resNum[i] = s;
            }
        }
        if (enlarge == 1) resNum.Insert(0, 1);
        res.DeleteNuls();
        return res;
    }

    private static BigInt Reduce(BigInt a, BigInt b)//вычитает из большего меньшее два бигинта
    {
        a.DeleteNuls(); b.DeleteNuls(); 
        b.EqualizeTheDigits(a.Number.Count);
        var res = new BigInt();
        var num1 = new List<int>(a.Number); var num2 = new List<int>(b.Number);
        for (var i = 0; i < a.Number.Count; i++)
        {
            var r = num1[i] - num2[i];
            if (r < 0)
            {
                num1[i + 1]--;
                r = 10 + r;
            }
            res.SetDigit(i, r);
        }
        res.DeleteNuls();
        return res;
    }

    public static BigInt Mod(BigInt a1, BigInt b) // деление по модулю
    {
        var a = new BigInt('+', new List<int>(a1.Number));
        a.DeleteNuls(); b.DeleteNuls();
        if (a < b) return a;
        if (a == b) return Zero;
        while (a > b)
        {                  
            var bSDopisNuls = new BigInt(b.Sign, new List<int>(b.Number));

            // 1,430,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000
            //14,299,957,056,376,767,329,454,880,514,019,834,315,878,107,616,003,372,189,510,312,530,372,009,184,902,888,961,739,623,919,010,110,377,987,011,442,493,486,117,202,360,415,845,666,384,627,768,436,296,772,219,009,176,743,399,772,868,636,439,042,064,384
            BigInt newraz;
            var razn = new BigInt((a.Number.Count - b.Number.Count).ToString());//1
            bSDopisNuls *= new BigInt("10").Pow(razn);
            if (bSDopisNuls > a)
                newraz = razn - One;//2
            else
                newraz = razn;
            var rabRazr = new BigInt("10").Pow(newraz);//3
            var newrazRes = b * rabRazr;
            for (var i = 1; b * new BigInt(i.ToString()) * rabRazr   <= a; ++i)
            {
                var r = rabRazr * new BigInt(i.ToString());
                newrazRes = r;
            }//4
            if (a >= newrazRes * b)
            {
                if (a - newrazRes * b < b)
                {

                    a -= newrazRes * b;
                    break;
                }
            }
            else break;
            a -= newrazRes * b;
        }
        a.DeleteNuls();
        return a;
    }

    public static BigInt Div(BigInt a1, BigInt b) // деление нацело
    {
        var a = new BigInt('+', new List<int>(a1.Number));
        a.DeleteNuls();b.DeleteNuls();
        if (a < b) return Zero;
        else if (a == b) return One;
        else
        {
            var res = new List<BigInt>();
            while (a > b)
            {
                var r = new BigInt(b.Sign, new List<int>(b.Number));
                var bSDopisNuls = new BigInt(b.Sign, new List<int>(b.Number));
                var newrazRes = Zero;
                var newraz = 0;
                var razn = a.Number.Count - b.Number.Count;//1
                bSDopisNuls = bSDopisNuls.MultOn10(razn);
                if (bSDopisNuls > a)
                    newraz = razn - 1;//2
                else
                    newraz = razn;
                var rabRazr = new BigInt('+' + (Math.Pow(10, newraz)).ToString());//3
                    
                for (var i = 1; b * rabRazr.MultOnDigit(i) < a; i++)
                {
                    r = rabRazr.MultOnDigit(i);
                    newrazRes = r;
                }//4
                //37812930162371623781924617294
                //33753333473491800000000000000
                if (a > newrazRes * b)
                {
                    if (a - newrazRes * b >= b)
                    {
                        r = r + One;
                        res.Add(r);
                    }
                    else
                    {
                        res.Add(r);
                    }
                }
                else break;
                a -= r * b;
            }
            var result = Zero;
            foreach (var e in res)
                result += e;
            result.Sign = a.Sign == b.Sign ? '+' : '-';
            return result;
        }
    }

    public BigInt MultOnDigit(int n) // умножение на цифру 0, ...,9
    {
        var sign = n;
        n = Math.Abs(n);
        var result = new BigInt('+', new List<int>());
        var t = 0; int r;
        for (var i = this.Number.Count - 1; i >= 0; i--)
        {
            r = this.Number[i] * n + t;
            {
                t = r / 10;
                r %= 10;
            }
            result.SetDigit(i, r);
        }
        if (t != 0) result.Number.Insert(0, t);
        result.DeleteNuls();
        if ((this.Sign == '+' && sign > 0) || (this.Sign == '-' && sign < 0))
            return result;
        else
            return -result;

    }

    public BigInt MultOn10(int n) // умножение на 10^n
    {
        this.Number.InsertRange(0, new int[n]);
        return this;
    }

    public static BigInt operator +(BigInt v1, BigInt v2)  // v1 + v2
    {
        if (v1.Sign == '+' && v2.Sign == '+')
            return Addition(v1, v2);
        else if (v1.Sign == '+' && v2.Sign == '-')
            return v1 - v2.ChangedSign();
        else if (v1.Sign == '-' && v2.Sign == '+')
            return v2 - -v1;
        else
        {
            var r = Addition(v1, v2);
            r.Sign = '-';
            return r;
        }  
    }

    public static BigInt operator -(BigInt v1, BigInt v2)  // v1 - v2
    {
        if (v2.Sign == '-')
        {
            return v1 + v2.ChangedSign();
        }
        
        if (v1 >= v2)
        {
            return Reduce(v1, v2);
        }
        else
        {
            if (v1.Sign == '-') return -Addition(v1, v2);
            return -Reduce(v2, v1);
        }
    }

    public static BigInt operator *(BigInt v1, BigInt v2)  // v1 * v2
    {
        v1.DeleteNuls(); v2.DeleteNuls();
        var result = new BigInt();
        result.EqualizeTheDigits(v1.Number.Count + v2.Number.Count);
        for (var i = 0; i < v1.Number.Count; ++i)
        for (int j = 0, carry = 0; j < v2.Number.Count || carry > 0; ++j)
        {
            var cur = result.Number[i + j] + v1.Number[i] * (j < v2.Number.Count ? v2.Number[j] : 0) + carry;
            result.Number[i + j] = cur % 10;
            carry = cur / 10;
        }
        while (result.Number.Count > 1 && result.Number.Last() == 0)
            result.Number.RemoveAt(result.Number.Count - 1);
        result.Sign = v1.Sign == v2.Sign ? '+' : '-';
        result.DeleteNuls();
        return result;
    }

    public BigInt Pow(BigInt a)
    {
        this.DeleteNuls();a.DeleteNuls();
        var res = new BigInt(Sign, new List<int>(Number));
        var a1 = new BigInt(a.Sign, new List<int>(a.Number));
        if (a1 == Zero) return One;
        for (;a1 > One; a1 -= One)
            res *= this;
        return res;
    }
                                             
    public static BigInt operator -(BigInt obj)   // унарный минус  -obj
    {
        return obj.Sign == '+'? new BigInt('-', obj.Number) : new BigInt('+', obj.Number);
    }
    
    //Операторы сравнения

    private static int Compare(BigInt v1, BigInt v2)
    {
        if (v1.Sign == '+' && v2.Sign == '-')
            return 1;
        else if (v2.Sign == '+' && v1.Sign == '-')
            return -1;
        else if (v1.Sign == '+' && v2.Sign == '+')
        {
            return CompareWithoutSign(v1, v2);
        }
        else
            return -1 * CompareWithoutSign(v1, v2);
    }

    private static int CompareWithoutSign(BigInt v1, BigInt v2)
    {
        v1.DeleteNuls();v2.DeleteNuls();
        if (v1.Number.Count > v2.Number.Count)
            return 1;
        else if (v1.Number.Count < v2.Number.Count)
            return -1;
        else
        {
            for (var i = v1.Number.Count-1; i >= 0; i--)
            {
                if (v1.Number[i] > v2.Number[i])
                    return 1;
                else if (v1.Number[i] < v2.Number[i])
                    return -1;
            }
            return 0;
        }
    }

    public bool IsSimple()//проверяет, является ли число простым
    {
        if (this < new BigInt("2"))
            return false;

        if (this == new BigInt("2"))
            return true;

        for (var i = new BigInt("2"); i < this; i += new BigInt("1"))
        {
            if (this % i == Zero)
                return false;
        }
        return true;
    }

    public static BigInt FindModInverse(BigInt a, BigInt n)//вычисляет обратное по модулю
    {
        if (a + One == n) return a;
        var x = new BigInt(); var y = new BigInt();
        ExtendedEuclid(a, n, out x, out y);
        return (x % n + n) % n;
    }

    private static BigInt ExtendedEuclid(BigInt a, BigInt b, out BigInt x, out BigInt y)//расширенный алгоритм Евклида
    {
            
        if (a == new BigInt())
        {
            x = new BigInt();
            y = new BigInt("1");
            return b;
        }
        var newY = new BigInt();
        var newX = new BigInt();
        var gcd = ExtendedEuclid(b % a, a, out newX,out newY);
            
        x = newY - (b / a) * newX;
        y = newX;

        return gcd;
    }
    
    public static BigInt operator /(BigInt a, BigInt b) => Div(a, b);
    
    public static BigInt operator %(BigInt a, BigInt b) => Mod(a, b);
    
    public static bool operator <(BigInt a, BigInt b) => Compare(a, b) < 0;
    
    public static bool operator >(BigInt a, BigInt b) => Compare(a, b) > 0;
    
    public static bool operator <=(BigInt a, BigInt b) => Compare(a, b) <= 0;
    
    public static bool operator >=(BigInt a, BigInt b) => Compare(a, b) >= 0;
    
    public static bool operator ==(BigInt a, BigInt b) => Compare(a, b) == 0;

    public static bool operator !=(BigInt a, BigInt b) => Compare(a, b) != 0;
}