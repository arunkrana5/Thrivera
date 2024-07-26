using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDbWebApi.Models
{ 
    public class clsReportResultsInformation
    {
        public string ColValue1 { get; set; }
        public string ColValue2 { get; set; }
        public string type { get; set; }
    }
     
    public class clsHeaderTemplate
    {
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string PatientMiddleName { get; set; }
        public string PatientDateofbirth { get; set; }
        public string PatientGender { get; set; }
        public int AccessionNo { get; set; }
        //public string Logo { get; set; }
        //public clsPatientInformation PatientInformation { get; set; }
        //clsSpecimenInformation SpecimenInformation { get; set; }
        //clsPhysicianInformation PhysicianInformation { get; set; }
        //clsLaboratoryInformation LaboratoryInformation { get; set; }
    }

    public class clsResults
    {
        public string ResultName { get; set; }
        public string Result { get; set; }

        public clsResults(string _ResultName , string _Result)
        {
            ResultName = _ResultName;
            Result = _Result;
        }
    }

    public class ReportFields
    {
        public string Specimen { get; set; }
        public string Interpretation { get; set; }
        public string PhysicianComments { get; set; }
        public string ClinicalHistory { get; set; }
        public List<clsResults> Results { get; set; }
        public List<string> ResultImages { get; set; }

        public clsHeaderTemplate headerObj { get; set; }

        public ReportFields()
        {
            Results = new List<clsResults>();
            ResultImages = new List<string>();
        }
    }

    public class clsPatientInformation
    {
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string PateintMiddleName { get; set; }
        public string PateintDateofbirth { get; set; }
        public string PatientGender { get; set; }
        public int AccessionNo { get; set; }
        public string ChartNo { get; set; }
    }

    internal class clsSpecimenInformation
    {
        string SecimenSite { get; set; }
        string SpecimenID { get; set; }
        string DateCollected { get; set; }
        string DateOrdered { get; set; }
        string DateReceived { get; set; }
        string DateReported { get; set; }
        string SecimenType { get; set; }
    }

    internal class clsPhysicianInformation
    {
        string PhysicianFirstName { get; set; }
        string PhysicianLastName { get; set; }
        string AdditionalPhysician { get; set; }
        string Institution { get; set; }
        string PhysicianPhone { get; set; }
        string PhysicianFax { get; set; }
    }

    internal class clsLaboratoryInformation
    {
        string LaboratoryPersonName { get; set; }
        string LaboratoryQualification { get; set; }
        string LaboratoryAddress { get; set; }
        string LaboratoryPhone { get; set; }
        string LaboratoryFax { get; set; }
    }
}