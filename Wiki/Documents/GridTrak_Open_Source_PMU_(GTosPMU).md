<h1>GridTrak Open Source PMU (GTosPMU) Sensor</h1>
<h2>Design Features</h2>
<p>The GridTrak Open Source PMU (GTosPMU) is a PMU sensor implementation that provides original design, firmware source code, software source code, and concepts.</p>
<ul>
<li>Transmits data in plain text or IEEE C37.118-2005 format</li>
<li>Measures Frequency and Phase Angle for 60Hz or 50Hz AC signals</li>
<li>Measurement is made by converting the AC wave form to square wave at discrete voltage reference points.</li>
<li>Calculates the magnitude for an ideal sine wave of the signal</li>
<li>Maximum sample rate = 30 samples per second. Reliable sample rate = 10 samples per second</li>
<li>GPS PPS Input</li>
<li>Internal Timers used when PPS is not available</li>
<li>Dual RS232 interfaces for Host PC and GPS communications</li>
<li>Uses the AC signal source as the circuit power supply</li>
<li>Optional jumper to isolate the AC signal from the circuit power supply (requires a separate DC power source for the circuit)</li>
<li>Includes schematics and ExpressPCB layout files</li>
</ul>
<h3>GridTrak Open Source PMU (GTosPMU) Limitations</h3>
<ul>
<li>Measures only one signal (i.e. 1 voltage frequency and phase)</li>
<li>Converts the signal to a square wave, so all harmonics are removed. If you want to measure wave form or harmonics, this is not the sensor to use because these are essentially filtered out from the input signal.</li>
<li>Intended for research and development experimentation and observation. Not intended for critical control applications</li>
</ul>
<h3>GridTrak Open Source PMU (GTosPMU) Packaged Kits are Available</h3>
<p>The GridTrak Open Source SynchroPhasor PMU (GTosPMU) Project is an on-going research and development project that is continually evolving. When you contact us to order kits or sensors you will be advised of the current project status prior to our accepting your order. <br /><br />To order GridTrak Open Source PMU kits or sensors, contact <a href="http://www.codeplex.com/site/users/view/ajstadlin">ajstadlin</a> for an up to date order form with current pricing.&nbsp; A rough price estimate (as of April 2012) is $800 for 3 unassembled kits with GPS.&nbsp; Lead time is about 3 weeks depending on availability of components from suppliers.<br /><br />The GridTrak Open Source SyncrhoPhasor PMU (GTosPMU) kits and sensor above are not commercial products or sales ventures. The project hosted here on CodePlex is intended to provide enough open source information for you to build your own SynchroPhasor PMU sensors. The GTosPMU packaged kits are priced to include the components and reasonable labor fee for the people procuring components, labeling, packaging, or assembling, and shipping the kit to you. This enables you to conveniently purchase all of the kit components pre-packaged, labeled, and ready for easy assembly or to just skip all the work and purchase an assembled open source PMU. <br /><br />The alternative to purchasing kits or sensor is to obtain your own components. All of the components are readily available from common electronics suppliers. The kit parts are all thru-hole and can also be assembled on a prototype breadboard. Many of the components may also be obtained in small quantities as free samples from their manufacturers. <br /><br />Note 1: Kits and Assemblies do not include 9V battery</p>
<p>Note 2: The Kits and Assemblies do not include RS232/USB/Serial cable or converter (USB/Serial converter will work for computers without RS232 ports). We have tested and recommend the USB-RS232 Converter, Model USA-FTDI-A12 made by GearMO. We have tested this product to be reliable for 24/7 operation for weeks on end with Windows 64bit OS's.</p>
<p>Note 3: Each Kit sensor includes 120 to 9VAC/AC Transformer, all electronic components it requires to operate, PCB, and preprogrammed dsPIC30F3013 micro controller.</p>
<p>Note 4: Suggested Tools (not included with kit): Decent soldering iron and solder, flush cut or decent wire cutters, phillips head screw driver, drill or roto tool for case modifications, dsPIC(16bit) device programmer and cable. Note: All of the components are thru-hole. The sensor can be constructed on a single 6" bread board.</p>
<p>Note 5:&nbsp; GridTrak cannot provide technical support for the GPS or its accessories.&nbsp; We encourage you to purchase the GPS directly from Garmin or your own supplier for better GPS technical support and product warranty.&nbsp; If you purchase the GPS through GridTrak, support will be limited to the its interfaces with the PMU; Time of Day data and PPS signal.</p>
<p>Photo of an Assembled GridTrak Open Source PMU (GTosPMU) Sensor Kit (original Model A without GPS)</p>
<p><img title="GTosPMU_Model_A_Assembly_web.JPG" src="http://i3.codeplex.com/Download?ProjectName=GridTrak&amp;DownloadId=206997" alt="GTosPMU_Model_A_Assembly_web.JPG" /></p>
<h4>Photo of a Packaged, Ready to Ship, GridTrak Open Source PMU (GTosPMU) Sensor Kit (for illustration only)</h4>
<p><img title="GTosPMU_Mod_A_Kit_web.JPG" src="http://i3.codeplex.com/Download?ProjectName=GridTrak&amp;DownloadId=206998" alt="GTosPMU_Mod_A_Kit_web.JPG" /></p>
<h4>GridTrak Open Source PMU (GTosPMU) Sensor Kit Assembled on a Prototype Board</h4>
<p><img title="GTosPMU_Rev_0_web.jpg" src="http://i3.codeplex.com/Download?ProjectName=GridTrak&amp;DownloadId=284411" alt="GTosPMU_Rev_0_web.jpg" /> <br />(note: The PPS simulator (bottom) is not included in the kit)</p>