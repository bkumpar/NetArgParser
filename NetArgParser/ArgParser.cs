using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetArgParser
{
    public class ArgParser
    {

        Dictionary<string, Argument> _arguments = new Dictionary<string, Argument>();
        string[] _args;
        int _lastArgIndex;
        List<string> _helpFlags = new List<string>();
        List<string> _helpDescriptions = new List<string>();
        int _helpFlagsLen;
        string _helpText;


        public ArgParser(string[] args)
        {
            _args = args;
            _helpFlagsLen = 0;
        }

        public ArgParser(string[] args, string helpText)
        {
            _args = args;
            _helpFlagsLen = 0;
            _helpText = helpText;

        }

        public void add(Argument argItem)
        {
            string shortNameKey = argItem._shortName;
            _arguments[shortNameKey] = argItem;
            string helpFlag = (argItem._hasValue ? shortNameKey + " value " : shortNameKey);
            string longNameKey = argItem._longName;
            if (!longNameKey.Equals(""))
            {
                _arguments[longNameKey] = argItem;
                helpFlag += (argItem._hasValue ? " " + longNameKey + " value " : " " + longNameKey);
            }
            _helpFlags.Add(helpFlag);
            _helpDescriptions.Add(argItem._description);
            _helpFlagsLen = Math.Max(_helpFlagsLen, helpFlag.Length);
        }

        public string help()
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine(_helpText);
            for (int i = 0; i < _helpFlags.Count; i++)
            {
                output.Append(" ");
                output.Append(_helpFlags[i]);
                output.Append(new string(' ', _helpFlagsLen - _helpFlags[i].Length + 1));
                output.AppendLine(_helpDescriptions[i]);
            }
            return output.ToString();
        }

        public bool parse()
        {
            _lastArgIndex = 0;
            for (int i = 0; i < _args.Length; i++)
            {
                string paramName = _args[i];
                if (_arguments.Keys.Contains(paramName))
                {
                    _lastArgIndex = Math.Max(_lastArgIndex, i);
                    Argument it = _arguments[paramName];
                    it._found = true;
                    if (it._hasValue)
                    {
                        if (++i < _args.Length)
                        {
                            string paramValue = _args[i];
                            if (!_arguments.Keys.Contains(paramValue)) // paramValue isn't equal to any other paramName
                            {
                                it._value = paramValue;
                                _lastArgIndex = Math.Max(_lastArgIndex, i);
                            }
                            else
                            {
                                Console.WriteLine(String.Format("Missing value for parameter: {0}", paramName));
                                return false;
                            }
                        }
                        else
                        {
                            Console.WriteLine(String.Format("Error parsing argument: {0}", paramName));
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public List<string> getTailArguments()
        {
            List<string> tail = new List<string>();
            for (int i = _lastArgIndex + 1; i < _args.Length; i++)
            {
                tail.Add(_args[i]);
            }
            return tail;
        }
    }
}
