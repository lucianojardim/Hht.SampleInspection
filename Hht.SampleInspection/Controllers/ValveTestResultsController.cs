using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hht.SampleInspection.Models;
using System.Net.Mail;

namespace Hht.SampleInspection.Controllers
{
    public class ValveTestResultsController : Controller
    {
        private SampleInspectionEntities db = new SampleInspectionEntities();

        // GET: ValveTestResults
        public ActionResult Index()
        {
            var valveTestResults = db.ValveTestResults.Include(v => v.PartReceived).Include(v => v.PassFail03).Include(v => v.PassFail04).Include(v => v.PassFail08).Include(v => v.PassFail11).Include(v => v.PassFail13);
            return View(valveTestResults.ToList());
        }

        // GET: ValveTestResults/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ValveTestResult valveTestResult = db.ValveTestResults.Find(id);
            if (valveTestResult == null)
            {
                return HttpNotFound();
            }
            return View(valveTestResult);
        }

        // GET: ValveTestResults/Create
        public ActionResult Create()
        {
            ViewBag.PartReceivedId = new SelectList(db.PartReceiveds, "PartReceivedId", "SerialNumber");
            ViewBag.Step03TestResultId = new SelectList(db.PassFail03, "PassFail03Id", "PassFail03Desc");
            ViewBag.Step04TestResultId = new SelectList(db.PassFail04, "PassFail04Id", "PassFail04Desc");
            ViewBag.Step08TestResultId = new SelectList(db.PassFail08, "PassFail08Id", "PassFail08Desc");
            ViewBag.Step11TestResultId = new SelectList(db.PassFail11, "PassFail11Id", "PassFail11Desc");
            ViewBag.Step13TestResultId = new SelectList(db.PassFail13, "PassFail13Id", "PassFail13Desc");
            return View();
        }

        // POST: ValveTestResults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PartReceivedId,Step03TestResultId,Step04TestResultId,Step05mH,Step06mH,Step13TestResultId,Step10High,Step10Low,Step11TestResultId,Step08TestResultId")] ValveTestResult valveTestResult)
        {
            if (ModelState.IsValid)
            {
                db.ValveTestResults.Add(valveTestResult);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PartReceivedId = new SelectList(db.PartReceiveds, "PartReceivedId", "SerialNumber", valveTestResult.PartReceivedId);
            ViewBag.Step03TestResultId = new SelectList(db.PassFail03, "PassFail03Id", "PassFail03Desc", valveTestResult.Step03TestResultId);
            ViewBag.Step04TestResultId = new SelectList(db.PassFail04, "PassFail04Id", "PassFail04Desc", valveTestResult.Step04TestResultId);
            ViewBag.Step08TestResultId = new SelectList(db.PassFail08, "PassFail08Id", "PassFail08Desc", valveTestResult.Step08TestResultId);
            ViewBag.Step11TestResultId = new SelectList(db.PassFail11, "PassFail11Id", "PassFail11Desc", valveTestResult.Step11TestResultId);
            ViewBag.Step13TestResultId = new SelectList(db.PassFail13, "PassFail13Id", "PassFail13Desc", valveTestResult.Step13TestResultId);
            return View(valveTestResult);
        }

        // GET: ValveTestResults/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ValveTestResult valveTestResult = db.ValveTestResults.Find(id);
            if (valveTestResult == null)
            {
                return HttpNotFound();
            }
            ViewBag.PartReceivedId = new SelectList(db.PartReceiveds, "PartReceivedId", "SerialNumber", valveTestResult.PartReceivedId);
            ViewBag.Step03TestResultId = new SelectList(db.PassFail03, "PassFail03Id", "PassFail03Desc", valveTestResult.Step03TestResultId);
            ViewBag.Step04TestResultId = new SelectList(db.PassFail04, "PassFail04Id", "PassFail04Desc", valveTestResult.Step04TestResultId);
            ViewBag.Step08TestResultId = new SelectList(db.PassFail08, "PassFail08Id", "PassFail08Desc", valveTestResult.Step08TestResultId);
            ViewBag.Step11TestResultId = new SelectList(db.PassFail11, "PassFail11Id", "PassFail11Desc", valveTestResult.Step11TestResultId);
            ViewBag.Step13TestResultId = new SelectList(db.PassFail13, "PassFail13Id", "PassFail13Desc", valveTestResult.Step13TestResultId);

            // 2016-03-30 LCJ Assign pass or fail status to numerical tests to allow for visual cues to be displayed  
            ValidateValveNumericalTests(valveTestResult);

            TempData["alertMessage"] = "An email will be sent to the QA team if a valve fails a test.";
            return View(valveTestResult);
        }

        // POST: ValveTestResults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string button, [Bind(Include = "PartReceivedId,Step03TestResultId,Step04TestResultId,Step05mH,Step06mH,Step13TestResultId,Step10High,Step10Low,Step11TestResultId,Step08TestResultId")] ValveTestResult valveTestResult)
        {
            ViewBag.PartReceivedId = new SelectList(db.PartReceiveds, "PartReceivedId", "SerialNumber", valveTestResult.PartReceivedId);
            ViewBag.Step03TestResultId = new SelectList(db.PassFail03, "PassFail03Id", "PassFail03Desc", valveTestResult.Step03TestResultId);
            ViewBag.Step04TestResultId = new SelectList(db.PassFail04, "PassFail04Id", "PassFail04Desc", valveTestResult.Step04TestResultId);
            ViewBag.Step08TestResultId = new SelectList(db.PassFail08, "PassFail08Id", "PassFail08Desc", valveTestResult.Step08TestResultId);
            ViewBag.Step11TestResultId = new SelectList(db.PassFail11, "PassFail11Id", "PassFail11Desc", valveTestResult.Step11TestResultId);
            ViewBag.Step13TestResultId = new SelectList(db.PassFail13, "PassFail13Id", "PassFail13Desc", valveTestResult.Step13TestResultId);

            // 2016-03-30 LCJ Assign pass or fail status to numerical tests to allow for visual cues to be displayed  
            ValidateValveNumericalTests(valveTestResult);

            if (ModelState.IsValid)
            {
                db.Entry(valveTestResult).State = EntityState.Modified;
                db.SaveChanges();

                if (button == "Validate")
                {
                    // 2016-03-10 LCJ Changed to return to the PartsReceived index view instead of the ValveTestResults index view
                    return RedirectToAction("Edit", "ValveTestResults", new { id = valveTestResult.PartReceivedId });
                }
                if (button == "Save")
                {
                    // 2016-03-30 LCJ Send email to QA team if a part fails a test
                    try
                    {
                        string step03TestResultDesc = db.PassFail03.Find((valveTestResult.Step03TestResultId)).PassFail03Desc;
                        string step04TestResultDesc = db.PassFail04.Find((valveTestResult.Step04TestResultId)).PassFail04Desc;
                        string step08TestResultDesc = db.PassFail08.Find((valveTestResult.Step08TestResultId)).PassFail08Desc;
                        string step11TestResultDesc = db.PassFail11.Find((valveTestResult.Step11TestResultId)).PassFail11Desc;
                        string step13TestResultDesc = db.PassFail13.Find((valveTestResult.Step13TestResultId)).PassFail13Desc;
                        if ((step03TestResultDesc == "fail") ||
                            (step04TestResultDesc == "fail") ||
                            (step08TestResultDesc == "fail") ||
                            (step11TestResultDesc == "fail") ||
                            (step13TestResultDesc == "fail") ||
                            (ViewBag.Step05mhResultDesc == "fail")  || 
                            (ViewBag.Step06mhResultDesc == "fail")  || 
                            (ViewBag.Step10LowResultDesc == "fail") || 
                            (ViewBag.Step10HighResultDesc == "fail"))
                        {
                            MailMessage mail = new MailMessage();
                            string emailAddress = GetAppSettingUsingConfigurationManager("SampleInspectionEmailAddress");
                            mail.From = new MailAddress("donotreply@hnicorp.com");
                            mail.To.Add(emailAddress);
                            mail.Subject = "Email from the Sample Inspection Application - Valve failed test";

                            string vendorDesc = db.PartReceiveds.Find(valveTestResult.PartReceivedId).Vendor.VendorDesc;
                            string serialNumber = db.PartReceiveds.Find(valveTestResult.PartReceivedId).SerialNumber;
                            string partNumber = db.Parts.Find(db.PartReceiveds.Find(valveTestResult.PartReceivedId).PartId).PartNumber;
                            mail.Body = "Valve with PartNumber=" + partNumber + " and SerialNumber="+serialNumber+" sold by Vendor=" + vendorDesc + " was found defective.\r\n\r\n";
                            mail.Body = mail.Body + "Test results:\r\n";
                            mail.Body = mail.Body + "Result03 - Visual Insp, damaged screws/debris=" + step03TestResultDesc + "\r\n";
                            mail.Body = mail.Body + "Result04 - Drop/Impact datage check=" + step04TestResultDesc + "\r\n";
                            mail.Body = mail.Body + "Result05 - Pilot valve solenoid inductance (mH)=" + valveTestResult.Step05mH.ToString() + " (" + ViewBag.Step05mhResultDesc + ")\r\n";
                            mail.Body = mail.Body + "Result06 - Main valve solenoid inductance (mH)=" + valveTestResult.Step06mH.ToString() + " ("+ ViewBag.Step06mhResultDesc + ")\r\n";
                            mail.Body = mail.Body + "Result08 - Flame goes out on burner and pilot=" + step08TestResultDesc + "\r\n";
                            mail.Body = mail.Body + "Result10 - Valve control pressure Low=" + valveTestResult.Step10Low.ToString() + " (" + ViewBag.Step10LowResultDesc + ")\r\n";
                            mail.Body = mail.Body + "Result10 - Valve control pressure High=" + valveTestResult.Step10High.ToString() + " (" + ViewBag.Step10HighResultDesc + ")\r\n";
                            mail.Body = mail.Body + "Result11 - Flame Height Adjustment Works=" + step11TestResultDesc + "\r\n";
                            mail.Body = mail.Body + "Result13 - Gas leaks at valve and pilot=" + step13TestResultDesc + "\r\n";

                            string smtpServerName = GetAppSettingUsingConfigurationManager("SmtpServerName");
                            SmtpClient smtpServer = new SmtpClient(smtpServerName);
                            int smtpServerPort = Int32.Parse(GetAppSettingUsingConfigurationManager("SmtpServerPort"));
                            smtpServer.Port = smtpServerPort;

                            smtpServer.Send(mail);
                        }
                    }
                    catch(Exception ex)
                    {
                        return View("HttGeneralError", new HandleErrorInfo(new Exception(ex.ToString()), "PartsReceived", "Create"));
                    }

                    // 2016-03-10 LCJ Changed to return to the PartsReceived index view instead of the ValveTestResults index view
                    return RedirectToAction("Create", "PartsReceived", new { id = valveTestResult.PartReceivedId });
                }
            }

            return View(valveTestResult);
        }

        // GET: ValveTestResults/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ValveTestResult valveTestResult = db.ValveTestResults.Find(id);
            if (valveTestResult == null)
            {
                return HttpNotFound();
            }
            return View(valveTestResult);
        }

        // POST: ValveTestResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ValveTestResult valveTestResult = db.ValveTestResults.Find(id);
            db.ValveTestResults.Remove(valveTestResult);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //20160330 LCJ Convert numerical tests into pass or fail (to be used to visualize results in green or red background
        void ValidateValveNumericalTests(ValveTestResult valveTestResult)
        {
            int partId = (db.PartReceiveds.First(item => item.PartReceivedId == valveTestResult.PartReceivedId)).PartId;
            if (db.Valves.Any(item => item.Step5mHMin <= valveTestResult.Step05mH && item.Step5mHMax >= valveTestResult.Step05mH && item.PartId == partId))
            {
                ViewBag.Step05mhResultDesc = "pass";
            }
            else
            {
                ViewBag.Step05mhResultDesc = "fail";
            }
            if (db.Valves.Any(item => item.Step6mHMin <= valveTestResult.Step06mH && item.Step6mHMax >= valveTestResult.Step06mH && item.PartId == partId))
            {
                ViewBag.Step06mhResultDesc = "pass";
            }
            else
            {
                ViewBag.Step06mhResultDesc = "fail";
            }
            if (db.Valves.Any(item => item.Step10LowMin <= valveTestResult.Step10Low && item.Step10LowMax >= valveTestResult.Step10Low && item.PartId == partId))
            {
                ViewBag.Step10LowResultDesc = "pass";
            }
            else
            {
                ViewBag.Step10LowResultDesc = "fail";
            }
            if (db.Valves.Any(item => item.Step10HighMin <= valveTestResult.Step10High && item.Step10HighMax >= valveTestResult.Step10High && item.PartId == partId))
            {
                ViewBag.Step10HighResultDesc = "pass";
            }
            else
            {
                ViewBag.Step10HighResultDesc = "fail";
            }
        }

        //20160330 LCJ Get AppSetting from web.config
        public static string GetAppSettingUsingConfigurationManager(string customField)
        {
            return System.Configuration.ConfigurationManager.AppSettings[customField];
        }
        public static string GetAppSetting(string customField)
        {
            System.Configuration.Configuration config =
                System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(null);
            if (config.AppSettings.Settings.Count > 0)
            {
                var customSetting = config.AppSettings.Settings[customField].ToString();
                if (!string.IsNullOrEmpty(customSetting))
                {
                    return customSetting;
                }
            }
            return null;
        }
    }
}
