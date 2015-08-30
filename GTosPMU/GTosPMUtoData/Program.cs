using System;
using System.Collections.Generic;
using System.Text;
using TVA;
using TVA.PhasorProtocols;

namespace GTosPMUtoData
{
  class Program
  {
    static MultiProtocolFrameParser parser;
    static long frameCount;

    static void Main(string[] args)
    {
        Console.WriteLine("GTosPMU to Data for openPDC Test");
        // Create a new protocol parser
        parser = new MultiProtocolFrameParser();

        // Attach to desired events
        parser.ConnectionAttempt += parser_ConnectionAttempt;
        parser.ConnectionEstablished += parser_ConnectionEstablished;
        parser.ConnectionException += parser_ConnectionException;
        parser.ParsingException += parser_ParsingException;
        parser.ReceivedConfigurationFrame += parser_ReceivedConfigurationFrame;
        parser.ReceivedDataFrame += parser_ReceivedDataFrame;

        // Define the connection string
        parser.ConnectionString = "phasorProtocol=IeeeC37_118V1; transportProtocol=Serial; accessID=2; "
                                  + "port=COM1; baudrate=115200; parity=None; stopbits=One; databits=8; "
                                  + "dtrenable=False; rtsenable=False; autoStartDataParsingSequence = false;";

        // Start frame parser
        parser.AutoStartDataParsingSequence = true;
        parser.Start();

        // Hold the console open until the operator hits the <Enter> key
        Console.ReadLine();
    }


    static void parser_ReceivedDataFrame(object sender, EventArgs<IDataFrame> e)
    {
        // Increase the frame count each time a frame is received
        frameCount++;

        // Print information each time we receive 60 frames (every 2 seconds)
        if (frameCount % 60 == 0)
        {
            IDataCell device = e.Argument.Cells[0];
            Console.WriteLine("Received {0} data frames so far...", frameCount);
            Console.WriteLine("    Last frequency: {0}Hz", device.FrequencyValue.Frequency);
            Console.WriteLine("    Last Timestamp: {0}",
                ((DateTime)device.Timestamp).ToString("yyyy-MM-dd HH:mm:ss.fff"));
        }
    }


    static void parser_ReceivedConfigurationFrame(object sender, EventArgs<IConfigurationFrame> e)
    {
        // Notify the user when a configuration frame is received
        Console.WriteLine("Received configuration frame with {0} device(s)", e.Argument.Cells.Count);
    }


    static void parser_ParsingException(object sender, EventArgs<Exception> e)
    {
        // Output the exception to the user
        Console.WriteLine("Parsing exception: {0}", e.Argument);
    }


    static void parser_ConnectionException(object sender, EventArgs<Exception, int> e)
    {
        // Display which connection attempt failed and the exception that occurred
        Console.WriteLine("Connection attempt {0} failed due to exception: {1}", e.Argument2, e.Argument1);
    }


    static void parser_ConnectionEstablished(object sender, EventArgs e)
    {
        // Notify the user when the connection is established
        Console.WriteLine("Initiating {0} {1} based connection...",
            parser.PhasorProtocol.GetFormattedProtocolName(),
            parser.TransportProtocol.ToString().ToUpper());
    }


    static void parser_ConnectionAttempt(object sender, EventArgs e)
    {
        // Let the user know we are attempting to connect
        Console.WriteLine("Attempting connection...");
    }


  }
}
