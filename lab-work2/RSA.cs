using lab_work2;

class Rsa
    {
        readonly char[] _characters = new char[] { '#', 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И',
                                                'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С',
                                                'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ь', 'Ы', 'Ъ',
                                                'Э', 'Ю', 'Я', ' ', '1', '2', '3', '4', '5', '6', '7',
                                                '8', '9', '0', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H',
                                                'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S',
                                                'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        BigInt _d;
        BigInt _n;
        BigInt _m;

        string _input;

        string _decodeRes;
        List<string> _encodeRes;

        public Rsa(BigInt p, BigInt q, string s)
        {
            _input = s.ToUpper();
            if (p.IsSimple() && q.IsSimple())
            {
                _n = p * q;
                _m = ((p - BigInt.One) * (q - BigInt.One));
                var e = CalculateE(_m);
                _d = CalculateD(e, _m);


                _encodeRes = RsaEncode(_input, e, _n);

                _decodeRes = RsaDecode(_encodeRes, _d, _n);
            }
            else
                Console.WriteLine("p или q не простые числа");
        }

        public string GetAnswer()
        {
            return _decodeRes;
        }

        private string RsaDecode(List<string> encodeRes, BigInt d, BigInt n)
        {
            var res = "";
            
            
            foreach(var e in encodeRes)
            {
                var b = new BigInt(e);
                b = b.Pow(d);

                b = b % n;
                
                var index = Convert.ToInt32(b.ToString());

                res += _characters[index].ToString();
            }
            return res;
        }

        private List<string> RsaEncode(string s, BigInt e, BigInt n)
        { 
            var res = new List<string>();
            var b = new BigInt();

            for (var i = 0; i < s.Length; i++)
            {
                var index = Array.IndexOf(_characters, s[i]);

                b = new BigInt(index.ToString());
                b = b.Pow(e);

                b = b % n;

                res.Add(b.ToString());

            }
            return res;
        }

        private BigInt CalculateE(BigInt m)
        {

            var e = m - BigInt.One;

            for (var i = new BigInt("2"); i <= m; i += BigInt.One)
                if ((m % i == BigInt.Zero) && (e % i == BigInt.Zero)) //есть общий делитель
                {
                    e -= BigInt.One;
                    i = BigInt.One;
                }
            return e;
        }

        private BigInt CalculateD(BigInt e, BigInt m)
        {
            return BigInt.FindModInverse(e, m);
        }
    }
