using NetArgParser;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetArgParserTest
{
    class Program
    {
        static int Main(string[] args)
        {
            ArgParser parser = new ArgParser( args, "usage: argparser [options] filename(s)" ) ;
            VoidArgument arg1 = new VoidArgument("-h", false, "Shows help and exit");
            parser.add(arg1);
            IntArgument arg2 = new IntArgument("-s", "--seconds", true, "Set time in seconds");
            parser.add(arg2);
            IntArgument arg3 = new IntArgument("-m", "--minutes", true, "Set time in minutes");
            parser.add(arg3);
            VoidArgument arg4 = new VoidArgument("-x", "--exit", false, "Do nothing and exit");
            parser.add(arg4);
            DateTimeArgument arg5 = new DateTimeArgument("-d", "--datetime", true, "Set date and time");
            parser.add(arg5);
            //
            StringArgument arg6 = new StringArgument("-U", "--url", true, "odoo server address and port number (https://localhost:8069)");
            parser.add(arg6);
            StringArgument arg7 = new StringArgument("-D", "--database", true, "name of odoo database");
            parser.add(arg7);
            StringArgument arg8 = new StringArgument("-u", "--username", true, "username");
            parser.add(arg8);
            StringArgument arg9 = new StringArgument("-p", "--password", true, "password");
            parser.add(arg9);
            StringArgument arg10 = new StringArgument("-o", "--output_folder", true, "folder to unpack ortems data");
            parser.add(arg10);


            if (!parser.parse())
            {
                return 0;
            }

            if (arg1.found())
            {
                Console.WriteLine(parser.help());
                return 0;
            }
            if (arg2.found())
            {
                int time_s = arg2.value();
                int time_min = arg3.value();
                Console.WriteLine(String.Format("Set time to {0} [sec]",time_min * 60 + time_s ));
            }

            if (arg4.found())
                Console.WriteLine("Exit!");

            if (arg5.found())
            {
                CultureInfo cultureInfo = CultureInfo.CurrentCulture;

                DateTime dt = arg5.value();
                Console.WriteLine(String.Format("Timestamp: {0}", dt.ToString("g",cultureInfo)));
            }

            if(arg6.found())
            {
                Console.WriteLine(String.Format("url:{0}", arg6.value()));
            }
            else
            {
                Console.WriteLine("Missing url!");
            }

            if (arg7.found())
            {
                Console.WriteLine(String.Format("database:{0}", arg7.value()));
            }
            else
            {
                Console.WriteLine("Missing database!");
            }


            if (arg8.found())
            {
                Console.WriteLine(String.Format("username:{0}", arg8.value()));
            }
            else
            {
                Console.WriteLine("Missing username!");
            }

            if (arg9.found())
            {
                Console.WriteLine(String.Format("password:{0}", arg9.value()));
            }
            else
            {
                Console.WriteLine("Missing password!");
            }

            if (arg10.found())
            {
                Console.WriteLine(String.Format("output folder:{0}", arg10.value()));
            }
            else
            {
                Console.WriteLine("Missing output folder!");
            }


            List<string> fileNames = parser.getTailArguments();
            if (fileNames.Count == 0)
                Console.WriteLine("Missing file name(s)!");
            else if (fileNames.Count > 1)
                Console.WriteLine("Too many file name(s):");
            else
                Console.WriteLine(String.Format("File name:{0}", fileNames[0]));
            return 0;

        }
    }
}

/* test parameters:
-U "https://192.168.111.117:8069" -D tpt -u admin -p admin -o ortems_data ortems_data.zip

    
*/ 
