using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UnitConverter.Models;

namespace UnitConverter.Controllers
{
    public class ConverterController : Controller
    {
        [HttpGet]
        public IActionResult Wizard(){
            return View();
        }
        [HttpPost]
        public IActionResult Convert(UnitOperation unitOp){
            if(ModelState.IsValid){
                switch(unitOp.conversionType){
                    case "Speed":
                        switch(unitOp.inputType){
                            case "MPH":
                                //1.60934 is conversion rate from mph to km per hour (source: google convert)
                                unitOp.result = unitOp.input * (float)1.60934;
                                break;
                            case "KMPH":
                                //0.6213671 is conversion rate from kmph to mph (source: google convert)
                                unitOp.result = unitOp.input * (float)0.621371;
                                break;
                            default:
                                return View("Error");
                        }
                        break;
                    case "Temperature":
                        switch(unitOp.inputType){
                            case "Farenheit":
                                unitOp.result = (unitOp.input - (float)32) / (float)1.8;
                                break;
                            case "Celcius":
                                unitOp.result = unitOp.input * (float)1.8 + (float)32;
                                break;
                            default:
                                return View("Error");
                        }
                        break;
                }
            }else{
                return View("Error");
            }
            return View(unitOp);
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}