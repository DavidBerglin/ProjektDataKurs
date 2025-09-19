using Newtonsoft.Json;

namespace Seido.Utilities.SeedGenerator
{
    #region exported types
    public interface ISeed<T>
    {
        //In order to separate from real and seeded instances
        public bool Seeded { get; set; }

        //Seeded The instance
        public T Seed(SeedGenerator seedGenerator);
    }

    public class SeededLatin
    {
        public string Paragraph { get; init; }
        public List<string> Sentences { get; init; }
        public List<string> Words { get; init; }
    }

    public class SeededQuote
    {
        public string Quote { get; init; }
        public string Author { get; init; }
    }
    #endregion

    public class SeedGenerator : Random
    {
        readonly SeedJsonContent _seeds = null;

        #region Names
        public string PetName => _seeds.Names.PetNames[this.Next(0, _seeds.Names.PetNames.Count)];
        public string FirstName => _seeds.Names.FirstNames[this.Next(0, _seeds.Names.FirstNames.Count)];
        public string LastName => _seeds.Names.LastNames[this.Next(0, _seeds.Names.LastNames.Count)];
        public string FullName => $"{FirstName} {LastName}";
        #endregion

        #region Addresses
        public string Country => _seeds.Addresses[this.Next(0, _seeds.Addresses.Count)].Country;
        public string City(string Country = null)
        {
            if (Country != null)
            {
                var adr = _seeds.Addresses.FirstOrDefault(c => c.Country.ToLower() == Country.Trim().ToLower());
                if (adr == null)
                    throw new ArgumentException("Country not found");

                return adr.Cities[this.Next(0, adr.Cities.Count)];
            }

            var tmp = _seeds.Addresses[this.Next(0, _seeds.Addresses.Count)];
            return tmp.Cities[this.Next(0, tmp.Cities.Count)];
        }
        public string StreetAddress(string Country = null)
        {
            if (Country != null)
            {
                var adr = _seeds.Addresses.FirstOrDefault(c => c.Country.ToLower() == Country.Trim().ToLower());
                if (adr == null)
                    throw new ArgumentException("Country not found");

                return $"{adr.Streets[this.Next(0, adr.Streets.Count)]} {this.Next(1, 100)}";
            }

            var tmp = _seeds.Addresses[this.Next(0, _seeds.Addresses.Count)];
            return $"{tmp.Streets[this.Next(0, tmp.Streets.Count)]} {this.Next(1, 100)}";
        }
        public int ZipCode => this.Next(10101, 100000);
        #endregion

        #region Emails and phones
        public string Email(string fname = null, string lname = null)
        {
            fname ??= FirstName;
            lname ??= LastName;

            return $"{fname}.{lname}@{_seeds.Domains.Domains[this.Next(0, _seeds.Domains.Domains.Count)]}";
        }

        public string PhoneNr => $"{this.Next(700, 800)} {this.Next(100, 1000)} {this.Next(100, 1000)}";
        #endregion

        #region Quotes
        public List<SeededQuote> AllQuotes => _seeds.Quotes
            .Select(q => new SeededQuote { Quote = q.Quote, Author = q.Author })
            .ToList<SeededQuote>();

        public List<SeededQuote> Quotes(int tryNrOfItems)
        {
            return UniqueIndexPickedFromList(tryNrOfItems, AllQuotes);
        }

        public SeededQuote Quote => Quotes(1).FirstOrDefault();

        #endregion

        #region Latin
        public List<SeededLatin> AllLatin => _seeds.Latin
            .Select(l => new SeededLatin { Paragraph = l.Paragraph, Sentences = l.Sentences, Words = l.Words })
            .ToList();

        public List<SeededLatin> LatinParagraphs(int tryNrOfItems)
        {
            return UniqueIndexPickedFromList(tryNrOfItems, AllLatin);
        }

        public List<string> LatinSentences(int tryNrOfItems)
        {
            var sRet = new List<string>();
            for (int i = 0; i < tryNrOfItems; i++)
            {
                var pIdx = this.Next(0, AllLatin.Count);
                var sIdx = this.Next(0, AllLatin[pIdx].Sentences.Count);

                sRet.Add(AllLatin[pIdx].Sentences[sIdx]);
            }
            return sRet;
        }

        public List<string> LatinWords(int tryNrOfItems)
        {
            var sRet = new List<string>();
            for (int i = 0; i < tryNrOfItems; i++)
            {
                var pIdx = this.Next(0, AllLatin.Count);
                var wIdx = this.Next(0, AllLatin[pIdx].Words.Count);

                sRet.Add(AllLatin[pIdx].Words[wIdx]);
            }
            return sRet;
        }

        public string LatinParagraph => LatinParagraphs(1).FirstOrDefault()?.Paragraph;
        public string LatinSentence => LatinSentences(1).FirstOrDefault();
        #endregion

        #region Music
        public string MusicGroupName => "The " + _seeds.Music.GroupNames[this.Next(0, _seeds.Music.GroupNames.Count)]
            + " " + _seeds.Music.GroupNames[this.Next(0, _seeds.Music.GroupNames.Count)];

        public string MusicAlbumName => _seeds.Music.AlbumPrefix[this.Next(0, _seeds.Music.AlbumPrefix.Count)]
            + " " + _seeds.Music.AlbumNames[this.Next(0, _seeds.Music.AlbumNames.Count)]
            + " " + _seeds.Music.AlbumNames[this.Next(0, _seeds.Music.AlbumNames.Count)]
            + " " + _seeds.Music.AlbumSuffix[this.Next(0, _seeds.Music.AlbumSuffix.Count)];
        #endregion

        #region DateTime, bool and decimal
        public DateTime DateAndTime(int? fromYear = null, int? toYear = null)
        {
            bool dateOK = false;
            DateTime _date = default;
            while (!dateOK)
            {
                fromYear ??= DateTime.Today.Year;
                toYear ??= DateTime.Today.Year + 1;

                try
                {
                    int year = this.Next(Math.Min(fromYear.Value, toYear.Value),
                        Math.Max(fromYear.Value, toYear.Value));
                    int month = this.Next(1, 13);
                    int day = this.Next(1, 32);

                    _date = new DateTime(year, month, day);
                    dateOK = true;
                }
                catch
                {
                    dateOK = false;
                }
            }

            return DateTime.SpecifyKind(_date, DateTimeKind.Utc);
        }

        public bool Bool => (this.Next(0, 10) < 5) ? true : false;

        public decimal NextDecimal(int _from, int _to) => this.Next(_from * 1000, _to * 1000) / 1000M;
        #endregion

        #region From own String, Enum and List<TItem>
        public string FromString(string _inputString, string _splitDelimiter = ", ")
        {
            var _sarray = _inputString.Split(_splitDelimiter);
            return _sarray[this.Next(0, _sarray.Length)];
        }
        public TEnum FromEnum<TEnum>() where TEnum : struct
        {
            if (typeof(TEnum).IsEnum)
            {

                var _names = typeof(TEnum).GetEnumNames();
                var _name = _names[this.Next(0, _names.Length)];

                return Enum.Parse<TEnum>(_name);
            }
            throw new ArgumentException("Not an enum type");
        }
        public TItem FromList<TItem>(List<TItem> items)
        {
            return items[this.Next(0, items.Count)];
        }
        #endregion

        #region Generate seeded List of TItem

        //ISeed<TItem> has to be implemented to use this method
        public List<TItem> ItemsToList<TItem>(int NrOfItems)
            where TItem : ISeed<TItem>, new()
        {
            //Create a list of seeded items
            var _list = new List<TItem>();
            for (int c = 0; c < NrOfItems; c++)
            {
                _list.Add(new TItem() { Seeded = true }.Seed(this));
            }
            return _list;
        }

        //Create a list of unique randomly seeded items
        public List<TItem> UniqueItemsToList<TItem>(int tryNrOfItems, List<TItem> appendToUnique = null)
            where TItem : ISeed<TItem>, IEquatable<TItem>, new()
        {
            //Create a list of uniquely seeded items
            HashSet<TItem> _set = (appendToUnique == null) ? new HashSet<TItem>() : new HashSet<TItem>(appendToUnique);

            while (_set.Count < tryNrOfItems)
            {
                var _item = new TItem() { Seeded = true }.Seed(this);

                int _preCount = _set.Count;
                int tries = 0;
                do
                {
                    _set.Add(_item);

                    if (_set.Count == _preCount)
                    {
                        //Item was already in the _set. Generate a new one
                        _item = new TItem() { Seeded = true }.Seed(this);
                        ++tries;

                        //Does not seem to be able to generate new unique item
                        if (tries > 5)
                            return _set.ToList();
                    }

                } while (_set.Count <= _preCount);
            }

            return _set.ToList();
        }

        //Pick a number of unique items from a list of TItem (the List does not have to be unique)
        //IEquatable<TItem> has to be implemented to use this method
        public List<TItem> UniqueItemsPickedFromList<TItem>(int tryNrOfItems, List<TItem> list)
        where TItem : IEquatable<TItem>
        {
            //Create a list of uniquely seeded items
            HashSet<TItem> _set = new HashSet<TItem>();

            while (_set.Count < tryNrOfItems)
            {
                var _item = list[this.Next(0, list.Count)];

                int _preCount = _set.Count;
                int tries = 0;
                do
                {
                    _set.Add(_item);

                    if (_set.Count == _preCount)
                    {
                        //Item was already in the _set. Pick a new one
                        _item = list[this.Next(0, list.Count)];
                        ++tries;

                        //Does not seem to be able to pick new unique item
                        if (tries > 5)
                            return _set.ToList();
                    }

                } while (_set.Count <= _preCount);
            }

            return _set.ToList();
        }

        //Pick a number of items, all with unique indexes, from a list of TItem
        public List<TItem> UniqueIndexPickedFromList<TItem>(int tryNrOfItems, List<TItem> list)
             where TItem : new()
        {
            //Create a hashed list of unique indexes
            HashSet<int> _set = new HashSet<int>();

            while (_set.Count < tryNrOfItems)
            {
                var _idx = this.Next(0, list.Count);

                int _preCount = _set.Count;
                int tries = 0;
                do
                {
                    _set.Add(_idx);

                    if (_set.Count == _preCount)
                    {
                        //Idx was already in the _set. Generate a new one
                        _idx = this.Next(0, list.Count);
                        ++tries;

                        //Does not seem to be able to generate new unique idx
                        if (tries > 5)
                            break;
                    }

                } while (_set.Count <= _preCount);
            }

            //I have now a set of unique idx
            //return a list of items from a list with indexes
            var retList = new List<TItem>();
            foreach (var item in _set)
            {
                retList.Add(list[item]);
            }
            return retList;
        }
        #endregion
 
        #region initialize master content
        SeedJsonContent CreateMasterSeedFile()
        {
            return new SeedJsonContent()
            {
                Quotes = new List<SeedQuote>
                {
                    //About Love
                   new SeedQuote
                        {
                            jsonQuote = "Really enjoyed my visit, the atmosphere was relaxed and welcoming.",
                            jsonAuthor = "Anna"
                        },
                        new SeedQuote
                        {
                            jsonQuote = "Nice place to stop by, not too crowded and easy to find.",
                            jsonAuthor = "Jonas"
                        },
                        new SeedQuote
                        {
                            jsonQuote = "Beautiful surroundings, perfect for a weekend visit.",
                            jsonAuthor = "Maja"
                        },
                        new SeedQuote
                        {
                            jsonQuote = "Had a great time here with my family, kids loved it.",
                            jsonAuthor = "Oskar"
                        },
                        new SeedQuote
                        {
                            jsonQuote = "Good mix of history and modern touches, worth checking out.",
                            jsonAuthor = "Elin"
                        },
                        new SeedQuote
                        {
                            jsonQuote = "Friendly people and a cozy vibe, I’ll definitely come back.",
                            jsonAuthor = "Unknown"
                        },
                        new SeedQuote
                        {
                            jsonQuote = "A hidden gem, wasn’t expecting much but it surprised me.",
                            jsonAuthor = "Karin"
                        },
                        new SeedQuote
                        {
                            jsonQuote = "Plenty to see and do, but also calm enough to just relax.",
                            jsonAuthor = "Lukas"
                        },
                        new SeedQuote
                        {
                            jsonQuote = "Great spot for photos, the view is amazing.",
                            jsonAuthor = "Sofia"
                        },
                        new SeedQuote
                        {
                            jsonQuote = "Simple, nice, and worth the detour.",
                            jsonAuthor = "Martin"
                        },
                        new SeedQuote
                        {
                            jsonQuote = "Could have been better maintained, but still enjoyable.",
                            jsonAuthor = "Nora"
                        },
                        new SeedQuote
                        {
                            jsonQuote = "Loved the mix of locals and tourists, felt authentic.",
                            jsonAuthor = "Isak"
                        },
                        new SeedQuote
                        {
                            jsonQuote = "One of those places you don’t forget, highly recommend.",
                            jsonAuthor = "Emma"
                        },
                        new SeedQuote
                        {
                            jsonQuote = "Good place for a short stop, nothing fancy but nice.",
                            jsonAuthor = "Patrik"
                        },
                        new SeedQuote
                        {
                            jsonQuote = "The atmosphere was calm and relaxing, exactly what I needed.",
                            jsonAuthor = "Miriam"
                        },
                        new SeedQuote
                        {
                            jsonQuote = "Great for a couple of hours, especially if the weather is nice.",
                            jsonAuthor = "David"
                        },
                        new SeedQuote
                        {
                            jsonQuote = "I liked it, but it can get a bit busy at times.",
                            jsonAuthor = "Unknown"
                        },
                        new SeedQuote
                        {
                            jsonQuote = "Perfect little stop on our trip, glad we found it.",
                            jsonAuthor = "Hanna"
                        },
                        new SeedQuote
                        {
                            jsonQuote = "Nothing too special, but still worth a look.",
                            jsonAuthor = "Anton"
                        },
                        new SeedQuote
                        {
                            jsonQuote = "Very charming place, felt warm and welcoming.",
                            jsonAuthor = "Linn"
                        }

                },
                Latin = new List<SeedLatin> {
                        new SeedLatin {  jsonParagraph = "A popular spot known for its lively atmosphere and scenic surroundings." },
                        new SeedLatin { jsonParagraph = "Famous for its welcoming vibe and long local traditions." },
                        new SeedLatin { jsonParagraph = "A beautiful location with rich history and modern amenities." },
                        new SeedLatin { jsonParagraph = "Known for exceptional service and unforgettable experiences." },
                        new SeedLatin { jsonParagraph = "A charming place that combines comfort with adventure." },
                        new SeedLatin { jsonParagraph = "Offers stunning views and peaceful relaxation opportunities." },
                        new SeedLatin { jsonParagraph = "A unique destination with something special for everyone." },
                        new SeedLatin { jsonParagraph = "Celebrated for its authentic culture and warm hospitality." },
                        new SeedLatin { jsonParagraph = "An exciting venue perfect for memorable occasions." },
                        new SeedLatin { jsonParagraph = "A wonderful place to explore and create lasting memories." },
            },
                Addresses = new List<SeedAddress>
                {
                        new SeedAddress {
                            jsonCountry = "Sweden",
                            jsonCities = "Stockholm, Göteborg, Malmö, Uppsala, Linköping, Örebro",
                            jsonStreets = "Svedjevägen, Ringvägen, Vasagatan, Odenplan, Birger Jarlsgatan, Äppelviksvägen, Kvarnbacksvägen"
                        },
                        new SeedAddress {
                            jsonCountry = "Norway",
                            jsonCities = "Oslo, Bergen, Trondheim, Stavanger, Dramen",
                            jsonStreets = "Bygdoy alle, Frognerveien, Pilestredet, Vidars gate, Sågveien, Toftes gate, Gardeveiend",
                    },
                        new SeedAddress {
                            jsonCountry = "Denmark",
                            jsonCities = "Köpenhamn, Århus, Odense, Aahlborg, Esbjerg",
                            jsonStreets = "Rolighedsvej, Fensmarkgade, Svanevej, Gröndalsvej, Githersgade, Classensgade, Moltekesvej"
                    },
                        new SeedAddress {
                            jsonCountry = "Finland",
                            jsonCities = "Helsingfors, Espoo, Tampere, Vaanta, Oulu",
                            jsonStreets = "Arkandiankatu, Liisankatu, Ruoholahdenkatu, Pohjoistranta, Eerikinkatu, Vauhtitie, Itainen Vaideki"
                    },
                },
                Names = new SeedNames
                {
                    jsonFirstNames = "Harry, Lord, Hermione, Albus, Severus, Ron, Draco, Frodo, Gandalf, Sam, Peregrin, Saruman",
                    jsonLastNames = "Potter, Voldemort, Granger, Dumbledore, Snape, Malfoy, Baggins, the Gray, Gamgee, Took, the White",
                    jsonPetNames = "Max, Charlie, Cooper, Milo, Rocky, Wanda, Teddy, Duke, Leo, Max, Simba",
                },
                Domains = new SeedDomains
                {
                    jsonDomainNames = "icloud.com, me.com, mac.com, hotmail.com, gmail.com"
                },
                Music = new SeedMusic
                {
                    jsonGroupNames = "Led, Zeppelin, Queen, Pink, Floyd, Creedence, Clearwater, Revival, " +
                        "Arosmith, Who, AC/DC, Rolling, Stones, Eagles, Deep, Purple, Prince, Dylan",
                    jsonAlbumNames = "Heaven, Rock, Moon, Cosmos, Walk, Hunky, Blue, Highway, " +
                        "Satisfaction, California, Stairway, Purple, Senor",
                    jsonAlbumPrefix = "A, The, One, The great, A wonderful, Let's rock with, Relaxing, Chill with, Dance with",
                    jsonAlbumSuffix = "with friends, with love, with fire, and walking, being happy",
                }
            };
        }
        #endregion

        #region create master json file
        public string WriteMasterStream()
        {
            return CreateMasterSeedFile().WriteFile("master-seeds.json");
        }
        #endregion

        #region contructors
        public SeedGenerator()
        {
            _seeds = CreateMasterSeedFile();
        }
        public SeedGenerator(string SeedPathName)
        {
            if (!SeedJsonContent.FileExists(SeedPathName))
            {
                throw new FileNotFoundException(SeedPathName);
            }
            _seeds = SeedJsonContent.ReadFile(SeedPathName);
        }
        #endregion

        #region internal classes
        class SeedLatin
        {
            #region Latin towards json file
            string _jsonParagraph;
            public string jsonParagraph
            {
                get => _jsonParagraph;
                set
                {
                    _jsonParagraph = value;
                    _sentences = new List<string>(_jsonParagraph.Split(". "))
                        .Select(s =>
                        {
                            var _sentence = s.Trim(new char[] { ' ', ',', '.' });
                            return _sentence + '.';
                        }).ToList();

                    _words = new List<string>(_jsonParagraph.Split(" "))
                        .Select(w => w.Trim(new char[] { ' ', ',', '.' })).ToList();
                }
            }
            #endregion

            [JsonIgnore]
            public string Paragraph => _jsonParagraph;

            List<string> _sentences;
            [JsonIgnore]
            public List<string> Sentences => _sentences;

            List<string> _words;
            [JsonIgnore]
            public List<string> Words => _words;
        }
        class SeedQuote
        {
            #region Quotes towards json file
            string _jsonQuote;
            public string jsonQuote { get => _jsonQuote; set => _jsonQuote = value; }

            string _jsonAuthor;
            public string jsonAuthor { get => _jsonAuthor; set => _jsonAuthor = value; }
            #endregion

            [JsonIgnore]
            public string Quote => _jsonQuote;
            [JsonIgnore]
            public string Author => _jsonAuthor;
        }
        class SeedAddress
        {
            #region Country towards json file
            string _jsonCountry;
            public string jsonCountry { get => _jsonCountry; set { _jsonCountry = value; }}
            #endregion

            [JsonIgnore]
            public string Country => _jsonCountry;

            #region Streets towards json file
            string _jsonStreets;
            public string jsonStreets
            {
                get => _jsonStreets;
                set
                {
                    _jsonStreets = value;
                    _streets = _jsonStreets.Split(", ").ToList();
                }
            }
            #endregion

            List<string> _streets;
            [JsonIgnore]
            public List<string> Streets => _streets;

            #region Cities towards json file
            string _jsonCities;
            public string jsonCities
            {
                get => _jsonCities;
                set
                {
                    _jsonCities = value;
                    _cities = _jsonCities.Split(", ").ToList();
                }
            }
            #endregion

            List<string> _cities;
            [JsonIgnore]
            public List<string> Cities => _cities;
        }
        class SeedNames
        {
            #region Names towards json file
            string _jsonFirstNames;
            public string jsonFirstNames
            {
                get => _jsonFirstNames;
                set
                {
                    _jsonFirstNames = value;
                    _firstNames = _jsonFirstNames.Split(", ").ToList();
                }
            }

            string _jsonLastNames;
            public string jsonLastNames
            {
                get => _jsonLastNames;
                set
                {
                    _jsonLastNames = value;
                    _lastNames = _jsonLastNames.Split(", ").ToList();
                }
            }

            string _jsonPetNames;
            public string jsonPetNames
            {
                get => _jsonPetNames;
                set
                {
                    _jsonPetNames = value;
                    _petNames = _jsonPetNames.Split(", ").ToList();
                }
            }
            #endregion

            List<string> _firstNames;
            [JsonIgnore]
            public List<string> FirstNames => _firstNames;

            List<string> _lastNames;
            [JsonIgnore]
            public List<string> LastNames => _lastNames;

            List<string> _petNames;
            [JsonIgnore]
            public List<string> PetNames => _petNames;
        }
        class SeedDomains
        {
            #region Domains towards json file
            string _jsonDomainNames;
            public string jsonDomainNames
            {
                get => _jsonDomainNames;
                set
                {
                    _jsonDomainNames = value;
                    _domainNames = _jsonDomainNames.Split(", ").ToList();
                }
            }
            #endregion

            List<string> _domainNames;
            [JsonIgnore]
            public List<string> Domains => _domainNames;
        }
        class SeedMusic
        {
            #region Music towards json file
            string _jsonGroupNames;
            public string jsonGroupNames
            {
                get => _jsonGroupNames;
                set
                {
                    _jsonGroupNames = value;
                    _groupNames = _jsonGroupNames.Split(", ").ToList();
                }
            }

            string _jsonAlbumNames;
            public string jsonAlbumNames
            {
                get => _jsonAlbumNames;
                set
                {
                    _jsonAlbumNames = value;
                    _albumNames = _jsonAlbumNames.Split(", ").ToList();
                }
            }

            string _jsonAlbumPrefix;
            public string jsonAlbumPrefix
            {
                get => _jsonAlbumPrefix;
                set
                {
                    _jsonAlbumPrefix = value;
                    _albumPrefix = _jsonAlbumPrefix.Split(", ").ToList();
                }
            }

            string _jsonAlbumSuffix;
            public string jsonAlbumSuffix
            {
                get => _jsonAlbumSuffix;
                set
                {
                    _jsonAlbumSuffix = value;
                    _albumSuffix = _jsonAlbumSuffix.Split(", ").ToList();
                }
            }
            #endregion

            List<string> _groupNames;
            [JsonIgnore]
            public List<string> GroupNames => _groupNames;

            List<string> _albumNames;
            [JsonIgnore]
            public List<string> AlbumNames => _albumNames;

            List<string> _albumPrefix;
            [JsonIgnore]
            public List<string> AlbumPrefix => _albumPrefix;

            List<string> _albumSuffix;
            [JsonIgnore]
            public List<string> AlbumSuffix => _albumSuffix;
        }

        class SeedJsonContent
        {
            public List<SeedQuote> Quotes { get; set; } = new List<SeedQuote>();
            public List<SeedLatin> Latin { get; set; } = new List<SeedLatin>();
            public List<SeedAddress> Addresses { get; set; } = new List<SeedAddress>();
            public SeedNames Names { get; set; } = new SeedNames();
            public SeedDomains Domains { get; set; } = new SeedDomains();
            public SeedMusic Music { get; set; } = new SeedMusic();


            public string WriteFile(string FileName) => WriteFile(this, FileName);
            public static string WriteFile(SeedJsonContent Seeds, string FileName)
            {
                var fn = fname(FileName);
                using (Stream s = File.Create(fn))
                using (TextWriter writer = new StreamWriter(s))
                {
                    writer.Write(JsonConvert.SerializeObject(Seeds, Formatting.Indented));
                }

                return fn;
            }

            public static SeedJsonContent ReadFile(string PathName)
            {
                SeedJsonContent seeds = null;
                using (Stream s = File.OpenRead(PathName))
                using (TextReader reader = new StreamReader(s))

                    seeds = JsonConvert.DeserializeObject<SeedJsonContent>(reader.ReadToEnd());

                return seeds;
            }

            static string fname(string name)
            {
                var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                documentPath = Path.Combine(documentPath, "SeedGenerator");
                if (!Directory.Exists(documentPath)) Directory.CreateDirectory(documentPath);
                return Path.Combine(documentPath, name);
            }

            public static bool FileExists(string FileName){

                var fn = Path.GetFileName(FileName);
                if (fn == FileName)
                {
                    //no path in FileName use default directory
                   return File.Exists(fname(FileName));
                }
    
                return File.Exists(FileName);
            }
        }
    #endregion
    }
}

