<html lang="en">
<body>
<!--HtmlToGmd.Body-->
<div id="NavigationMenu">
<h1><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/GridTrak_Home.md">
<img src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/GridTrak_Logo.png" alt="Open Source SynchroPhasor PMU" /></a></h1>
<hr />
<table style="width: 100%; border-collapse: collapse; border: 0px solid gray;">
<tr>
<td style="width: 25%; text-align:center;"><b><a href="http://www.gridtrak.com">GridTrak</a></b></td>
<td style="width: 25%; text-align:center;"><b><a href="https://github.com/ajstadlin/GridTrak">GridTrak on GitHub</a></b></td>
<td style="width: 25%; text-align:center;"><b><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/GridTrak_Home.md">GridTrak Wiki Home</a></b></td>
<td style="width: 25%; text-align:center;"><b><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/GridTrak_Documentation_Home.md">Documentation</a></b></td>
</tr>
</table>
</div>
<hr />
<!--/HtmlToGmd.Body-->

<div class="WikiContent">
<div class="wikidoc">
<h1 style="page-break-before:always; font-style:normal">Kit Assembly Procedure</h1>
<p style="font-style:normal"><strong><span style="color:#008000">Updated&nbsp;March 21, 2011</span></strong></p>
<p style="font-style:normal">* Tip:&nbsp; Add components from shortest in height&nbsp;to the&nbsp;tallest.<br>
* The author can assemble and test 1 sensor in about 10 hours or 3 sensors in about 20 hours. You are probably faster B-)</p>
<h3 style="font-style:normal">1. PCB Preparation for Case Mounting</h3>
<p style="font-style:normal">[ Approximately 30 minutes ]</p>
<p style="font-style:normal">A. Mount the PC Board in the case. Use a roto-tool or rat tail file to grind a radius in the board edge to allow screw clearance. There are small crescent etchings on the PCB marking where the radius clearance may be needed.</p>
<p style="font-style:normal">B. For a case bottom reset pin hole: mount the PC Board in the case and use the center hole in the Reset Button SW1 as a drill guide to drill a hole in the case bottom. Enlarge the case hole if desired.</p>
<p style="font-style:normal">C. Remove the PCB from the case mounting to complete the PCB assembly</p>
<h3 style="font-style:normal">2. Solder in the Sockets</h3>
<p style="font-style:normal">[ Approximately 30 minutes ]</p>
<p style="font-style:normal">U2 &ndash; 8 Pin Socket</p>
<p style="font-style:normal">X1 &ndash; 8 Pin Socket; Only Pins 1,4,5, and 8 are used. Trim off the middle Pins 2,3, 6, and 7.</p>
<p style="font-style:normal">U6 &ndash; 18 Pin Socket</p>
<p style="font-style:normal">U1 &ndash; 28 Pin Socket</p>
<h3 style="font-style:normal">3. Solder in the Pin Headers</h3>
<p style="font-style:normal">[ Approximately 30 minutes ]</p>
<p style="font-style:normal">J1 x 6</p>
<p style="font-style:normal">J2 x 2</p>
<p style="font-style:normal">J3 x 2</p>
<p style="font-style:normal">J4 x 3</p>
<p style="font-style:normal">J5 x 3</p>
<p style="font-style:normal">J8 x 3</p>
<p style="font-style:normal">J9 x 3</p>
<p style="font-style:normal">J10 x 10</p>
<p style="font-style:normal">J11 x 3</p>
<p style="font-style:normal">J30 x 3</p>
<p style="font-style:normal">CLK x 1 - Do this one last to prevent pushing it through when hot.&nbsp; Note:&nbsp; This pin is an optional test point pin.&nbsp; It is not required for operation of the sensor.</p>
<h3 style="font-style:normal">4. Solder in the Bypass Capacitors</h3>
<p style="font-style:normal">[ Approximately 30 minutes ]</p>
<h3 style="font-style:normal">5. Solder in the Diodes</h3>
<p style="font-style:normal">[ Approximately 30 minutes ]</p>
<h3 style="font-style:normal">6. Solder in the Resistors</h3>
<p style="font-style:normal">[ Approximately 30 minutes ]</p>
<h3 style="font-style:normal">7. Solder in the VAC and Battery Connectors</h3>
<p style="font-style:normal">[ Less than 15 minutes ]</p>
<p style="font-style:normal">J6 and J7</p>
<h3 style="font-style:normal">8. Solder in the Activity and Power&nbsp;LEDs</h3>
<p style="font-style:normal">[ Less than 15 minutes ]</p>
<h3 style="font-style:normal">9. Solder in the Electrolytic Capacitors</h3>
<p style="font-style:normal">[ Less than&nbsp;15 minutes ]</p>
<p style="font-style:normal">C17 - 10uF</p>
<p style="font-style:normal">C21 - 22uF</p>
<p style="font-style:normal">C20 &ndash; 470uf</p>
<h3 style="font-style:normal">10. Solder in the Voltage Reference and Voltage Regulator</h3>
<p style="font-style:normal">[ Approximately 15 minutes ]</p>
<p style="font-style:normal">Solder in the MCP1525 Voltage Reference in&nbsp;U7.&nbsp;</p>
<p style="font-style:normal">Solder in the LM7805 (or alternative) Voltage Regulator in U4.</p>
<p style="font-style:normal">Install AC Bypass Jumper&nbsp;J3 to share the AC input to power the 5VDC Circuit</p>
<p style="font-style:normal">Install Activity LED Jumper&nbsp;J5 on pins 2 and 3 (top pins nearer the LED)</p>
<p style="font-style:normal">Install Power LED Jumper&nbsp;J11 on pins 1 and 2 (left pins nearer the LED)</p>
<p style="font-style:normal">Install the Voltage Regulator Heat Sink (now or later)</p>
<h3 style="font-style:normal">11. AC-AC Transformer Connector and Cable Sub Assembly</h3>
<p style="font-style:normal">[ Approximately 30 minutes ]</p>
<p style="font-style:normal">Trim the J7 Connector&nbsp;Plug's Wire Leads to a convenient length (about 4 inches long)</p>
<p style="font-style:normal">Put about 3/4&quot; of heat shrink tubing over each wire</p>
<p style="font-style:normal">Solder the leads to the 2.1mm VAC Transformer Jack.&nbsp; This is an AC connection so only connect&nbsp;the Jack's inside pin and spring contact.&nbsp;
<strong>Do not connect the panel ground outside of the Jack to the wiring.&nbsp; </strong>
</p>
<p style="font-style:normal">If available, use hot air to shrink the tubing over the soldered connections to both insulate and strengthen the connection.</p>
<h3 style="font-style:normal">12. Power Supply and Voltage Reference Verification Test</h3>
<p style="font-style:normal">[ Approximately 15 minutes ]</p>
<p style="font-style:normal">Connect the VAC transformer and test the &#43;5 VDC and &#43;2.5 VDC on the J30 pins. If you get these values and no smoking components, then you are ready to install the oscillator and ICs!</p>
<h3 style="font-style:normal">13. Install the ICs</h3>
<p style="font-style:normal">[ Approximately 15 minutes ]</p>
<p style="font-style:normal">Orient&nbsp;the circuit board horizontally with the micro controller and reset button towards the left.&nbsp; In this orientation, the Pin #1 in all of the sockets is to the lower left and all of the Pin #1 in the headers is to
 the left.&nbsp;</p>
<p style="font-style:normal">U1 &ndash; dsPIC30F3013 Micro Controller</p>
<p style="font-style:normal">U2 &ndash; MCP6292 Operational Amplifier</p>
<p style="font-style:normal">U6 &ndash; MAX232A* or MAX242A**</p>
<p style="font-style:normal">* Note: The MAX232A has 16 pins and the socket is 18 pins. When using a MAX232A, insert the IC with the MAX232A Pin #1 in the 18 pin socket's Pin #2 position. With a MAX232 installed, the socket pins #1 and #18 are empty.</p>
<p style="font-style:normal">** Note: The GridTrak PMU design includes support for the MAX242 to enable resetting the RS232 transceiver if this is required for improved interfacing with GPS devices.</p>
<h3 style="font-style:normal">14.&nbsp; Install the Oscillator</h3>
<p>X1&nbsp;&ndash; Oscillator</p>
<p>An exception to the IC layout rule is the Oscillator.&nbsp; The Oscillator's orientation is optimized to reduce the length of its signal wire connection to the micro controller.&nbsp;&nbsp; The Oscillator has One Square Corner near its Pin #1 for orientation.&nbsp;
 Install the Oscillator with it's square corner toward the bottom left.&nbsp;</p>
<h3 style="font-style:normal">15. Final Assembly Power Check</h3>
<p style="font-style:normal">[ Less than 15 minutes ]</p>
<p style="font-style:normal">Connect the AC-AC Transformer and test the &#43;5 VDC and &#43;2.5 VDC on the J30 pins again.&nbsp; If the voltages are correct, then the ICs and Oscillator are correctly installed.&nbsp; After a few minutes, the only component
 that should get significantly warm is the voltage regulator.</p>
<p style="font-style:normal">This test will only take a few moments if all was done correctly.</p>
<h3 style="font-style:normal">16. Case Machining</h3>
<p style="font-style:normal">[ Approximately 120 minutes ]</p>
<p style="font-style:normal">Remove the PCB from the case.</p>
<p style="font-style:normal">Drill a hole in the case for the 2.1mm VAC Transformer Jack.&nbsp; A good place to locate this jack is in the bottom part of the case near the 4 rectifier diodes.</p>
<p style="font-style:normal">Cut a hole in the case for the &nbsp;DB9 RS232 connector. &nbsp;Make sure to position the hole locations to allow clearance for the inside case posts.&nbsp; My preference is to position the connector as close to the upper edge of
 the bottom part of the case as possible with the 5 solder cups on top where they can clear the mounted PCB.&nbsp; A good place to locate the DB9 connector is on the side near the reset switch; leaving enough room not to interfere with the PCB mounting screw.&nbsp;</p>
<p style="font-style:normal"><strong>Note:&nbsp; Position the DB9 connector to allow enough space to add a second DB9 or other connector in the future.&nbsp; A second connector may be convenient for direct communications between PC and GPS.</strong></p>
<p style="font-style:normal">I personally think that cutting the hole for the DB9 RS232 connector is the most challenging task of the sensor assembly!</p>
<h3 style="font-style:normal">17. Install the VAC Transformer Jack in the Case</h3>
<p style="font-style:normal">[ Approximately 5 minutes ]</p>
<p style="font-style:normal">Mount the 2.1mm Jack in the case.&nbsp; Do not run the wires near the voltage regulator heat sink.&nbsp; It is OK to tuck the wires under the PCB if you take care not to short them out on an untrimmed component.</p>
<h3 style="font-style:normal">18. Assemble the DB9 RS232 Connection</h3>
<p style="font-style:normal">[ Approximately 15 minutes ]</p>
<p style="font-style:normal">The DB9 Connector should be mounted close to the RS232 pin headers.</p>
<p style="font-style:normal">Cut 2 of the 7&rdquo; blue F-F jumper wires in half.&nbsp;</p>
<p style="font-style:normal">Put a short piece of heat shrink tubing on each wire and solder the cut ends into the DB9 Connector's # 2, 3, and 5 solder cups.</p>
<p style="font-style:normal">Optionally use hot air to shrink the tubing over the solder cups to strengthen the connections.</p>
<p style="font-style:normal">Mount the DB9 Connector in the case using the 4-40 screw posts, lock washers, and nuts.</p>
<h3 style="font-style:normal">19. Mount the Reset Button</h3>
<p style="font-style:normal">[ Approximately 5 minutes ]</p>
<p style="font-style:normal">Mounting the Reset Button on the top of the board is better for open case testing or operation of the sensor. Mounting the Reset Button on the bottom of the board enables it to be clicked through the pin-hole in the bottom of the
 case. There also is a Reset connector next to SW1 that can be used to wire up an external Reset Switch.</p>
<p style="font-style:normal">During normal power up and operation, the sensor does not require the Reset Button.</p>
<p style="font-style:normal">The Reset Button has spring formed legs. It can be inserted into the PCB and will stay in place, without soldering, for testing purposes.&nbsp; You can then decide how to permanently solder it later.&nbsp; There is also a pair of
 header pins that can be used to connect a case mounted reset switch (not included in the kit).</p>
<h3 style="font-style:normal">20. Mount the PCB in the Case</h3>
<p style="font-style:normal">[ Approximately 15 minutes ]</p>
<p style="font-style:normal">Mount the PCB in the case.</p>
<p style="font-style:normal">Connect the Transformer Jack assembly's plug to J7.</p>
<p style="font-style:normal">Connect the RS232 wires to the RS232-1 header pins.</p>
<p style="font-style:normal">Wire from DB9 # 5 connects to RS232-1 Header Pin #1</p>
<p style="font-style:normal">Wire from DB9 # 3 connects to RS232-1 Header Pin #2</p>
<p style="font-style:normal">Wire from DB9 # 2 connects to RS232-1 Header Pin #3</p>
<h3 style="font-style:normal">21. Optional Battery or Alternative 9VDC Power Wiring</h3>
<p style="font-style:normal">[ Approximately 30 minutes ]</p>
<p style="font-style:normal">Solder the J6 plug wires to the battery connector or alternative 9VDC power supply components or switches per your own custom requirements.</p>
<h3 style="font-style:normal">22. Install GPS PPS Jumper Wire</h3>
<p style="font-style:normal">[ Approximately&nbsp;1 minute ]</p>
<p style="font-style:normal">Use a blue F-F jumper wire to connect the J4, Pin 1 - PPS Pin to&nbsp;ground pin J11,&nbsp;Pin 3 (to the right side of the Power LED Jumper Pins).&nbsp; This jumper&nbsp;wire&nbsp;sets the micro controller's PPS input&nbsp;interrupt
 pin to&nbsp;steady state low.&nbsp; This wire&nbsp;is removed when the PPS is connected to a GPS or signal generator.</p>
<h3 style="font-style:normal">23. The PMU is Now Ready for Use</h3>
<p style="font-style:normal">[ Approximately 30 minutes ]</p>
<p style="font-style:normal">Put your tools away and clean up the mess you made!</p>
<p style="font-style:normal">&nbsp;</p>
</div>
</div>

<hr />
<div class="footer">
Last edited <span class="smartDate" title="3/21/2011 4:17:40 PM" LocalTimeTicks="1300749460">Mar 21, 2011 at 4:17 PM</span> by <a id="wikiEditByLink" href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a>, version 12<br />
<!--HtmlToGmd.Migration-->Migrated from <a href="http://gridtrak.codeplex.com/wikipage?title=GTosPMU%20Kit%20-%20Assembly%20Procedure">CodePlex</a> Oct 31, 2015 by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajs</a><!--/HtmlToGmd.Migration-->
</div>

<!--HtmlToGmd.Foot-->
<div id="copyright">
<hr />
Copyright 2015 <a href="http://www.gridtrak.com">GridTrak</a>
</div>
<!--/HtmlToGmd.Foot-->

</body>
</html>