using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetArgParser
{
    public class Argument
    {
        public bool _found;
        public string _value;

        public string _shortName;
        public string _longName;
        public bool _hasValue;
        public string _description;

        public Argument()
        {

        }

        public Argument(string shortName, string longName, bool hasValue, string description)
        {
            _shortName = shortName;
            _longName = longName;
            _found = false;
            _hasValue = hasValue;
            _description = description;
        }

        public Argument(string shortName, bool hasValue, string description)
        {
            _shortName = shortName;
            _longName = "";
            _found = false;
            _hasValue = hasValue;
            _description = description;
        }

        public bool found()
        {
            return _found;
        }
    }

    public class StringArgument : Argument
    {
        public StringArgument(string shortName, string longName, bool hasValue, string description) : base(shortName, longName, hasValue, description)
        {
        }

        public string value()
        {
            return _value;
        }
    }

    public class IntArgument : Argument
    {
        public IntArgument(string shortName, string longName, bool hasValue, string description) : base(shortName, longName, hasValue, description)
        {
        }

        public int value()
        {
            return int.Parse(_value);
        }
    }

    public class DateTimeArgument : Argument
    {
        public DateTimeArgument(string shortName, string longName, bool hasValue, string description) : base(shortName, longName, hasValue, description)
        {
        }

        public DateTime value()
        {
            CultureInfo cultureInfo = CultureInfo.CurrentCulture;
            return DateTime.Parse(_value, cultureInfo);
        }
    }

    public class DoubleArgument : Argument
    {
        public double value()
        {
            return double.Parse(_value);
        }
    }

    public class VoidArgument : Argument
    {
        public VoidArgument(string shortName, bool hasValue, string description) : base(shortName, hasValue, description)
        {
        }

        public VoidArgument(string shortName, string longName, bool hasValue, string description) : base(shortName, longName, hasValue, description)
        {
        }
    }
}
