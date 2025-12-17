using System;
using System.Collections.Generic;

// ====================== 1. BASE PATIENT CLASS ======================
public abstract class HospitalPatient
{
    public int PatientID { get; private set; }
    public string FullName { get; private set; }
    public int PatientAge { get; private set; }
    public string PatientGender { get; private set; }

    protected HospitalPatient(int id, string name, int age, string gender)
    {
        PatientID = id;
        FullName = name;
        PatientAge = age;
        PatientGender = gender;
    }

    public abstract double GetBillAmount();

    public virtual void DisplayInfo()
    {
        Console.WriteLine($"ID: {PatientID}, Name: {FullName}, Age: {PatientAge}, Gender: {PatientGender}");
    }
}

// ====================== 2. PATIENT TYPES ======================
public class Inpatient : HospitalPatient
{
    public double WardCharges { get; private set; }

    public Inpatient(int id, string name, int age, string gender, double wardCharges = 1500)
        : base(id, name, age, gender)
    {
        WardCharges = wardCharges;
    }

    public override double GetBillAmount() => WardCharges;

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Type: Inpatient, Ward Charges: {WardCharges:C}");
    }
}

public class Outpatient : HospitalPatient
{
    public double DoctorFee { get; private set; }

    public Outpatient(int id, string name, int age, string gender, double doctorFee = 500)
        : base(id, name, age, gender)
    {
        DoctorFee = doctorFee;
    }

    public override double GetBillAmount() => DoctorFee;

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Type: Outpatient, Consultation Fee: {DoctorFee:C}");
    }
}

public class EmergencyPatient : HospitalPatient
{
    public double EmergencyFee { get; private set; }

    public EmergencyPatient(int id, string name, int age, string gender, double emergencyFee = 2000)
        : base(id, name, age, gender)
    {
        EmergencyFee = emergencyFee;
    }

    public override double GetBillAmount() => EmergencyFee;

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Type: Emergency, Emergency Fee: {EmergencyFee:C}");
    }
}

// ====================== 3. BILLING STRATEGY ======================
public delegate double BillingMethod(HospitalPatient patient);

public class HospitalBilling
{
    public double StandardBilling(HospitalPatient patient) => patient.GetBillAmount() * 1.10;
    public double EmergencyBilling(HospitalPatient patient) => patient.GetBillAmount() * 1.20;
    public double InsuranceBilling(HospitalPatient patient) => patient.GetBillAmount() * 0.70;
}

// ====================== 4. HOSPITAL MANAGEMENT ======================
public class HospitalSystem
{
    public event Action<HospitalPatient> PatientRegistered;
    public event Action<HospitalPatient, double> BillCompleted;

    private List<HospitalPatient> allPatients = new List<HospitalPatient>();

    public void RegisterPatient(HospitalPatient patient)
    {
        allPatients.Add(patient);
        Console.WriteLine($"\n {patient.FullName} has been registered.");
        PatientRegistered?.Invoke(patient);
    }

    public void ProcessBill(HospitalPatient patient, BillingMethod method)
    {
        double total = method(patient);
        Console.WriteLine($"💰 Bill for {patient.FullName}: {total:C}");
        BillCompleted?.Invoke(patient, total);
    }

    public void ShowPatients()
    {
        if (allPatients.Count == 0)
        {
            Console.WriteLine("\nNo patients registered yet.");
            return;
        }

        Console.WriteLine("\n--- All Registered Patients ---");
        foreach (var patient in allPatients)
        {
            patient.DisplayInfo();
            Console.WriteLine("----------------------------");
        }
    }

    public HospitalPatient FindPatientByID(int id)
    {
        return allPatients.Find(p => p.PatientID == id);
    }
}

// ====================== 5. DEPARTMENTS ======================
public class Laboratory
{
    public void NotifyAdmission(HospitalPatient patient) => Console.WriteLine($"Lab: Prepare tests for {patient.FullName}");
    public void NotifyBill(HospitalPatient patient, double amount) => Console.WriteLine($"Lab: Bill recorded for {patient.FullName}: {amount:C}");
}

public class Pharmacy
{
    public void NotifyAdmission(HospitalPatient patient) => Console.WriteLine($"Pharmacy: Prepare medicines for {patient.FullName}");
    public void NotifyBill(HospitalPatient patient, double amount) => Console.WriteLine($"Pharmacy: Bill recorded for {patient.FullName}: {amount:C}");
}

public class AccountsDept
{
    public void NotifyBill(HospitalPatient patient, double amount) => Console.WriteLine($"Accounts: Processed bill for {patient.FullName} = {amount:C}");
}

// ====================== 6. MAIN MENU ======================
class HospitalProgram
{
    static void Main()
    {
        HospitalSystem system = new HospitalSystem();
        HospitalBilling billing = new HospitalBilling();

        Laboratory lab = new Laboratory();
        Pharmacy pharmacy = new Pharmacy();
        AccountsDept accounts = new AccountsDept();

        system.PatientRegistered += lab.NotifyAdmission;
        system.PatientRegistered += pharmacy.NotifyAdmission;

        system.BillCompleted += lab.NotifyBill;
        system.BillCompleted += pharmacy.NotifyBill;
        system.BillCompleted += accounts.NotifyBill;

        Console.WriteLine("=== Welcome to Hospital Patient Management System ===");

        int patientCounter = 1;

        while (true)
        {
            Console.WriteLine("\n--- MENU ---");
            Console.WriteLine("1. Register New Patient");
            Console.WriteLine("2. View All Patients");
            Console.WriteLine("3. Generate Bill for a Patient");
            Console.WriteLine("4. Exit");
            Console.Write("Select an option: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine($"\n--- Enter details for Patient {patientCounter} ---");
                    Console.Write("Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Age: ");
                    int age = int.Parse(Console.ReadLine());
                    Console.Write("Gender (Male/Female): ");
                    string gender = Console.ReadLine();

                    Console.WriteLine("Select patient type: 1. Outpatient  2. Inpatient  3. Emergency");
                    int type = int.Parse(Console.ReadLine());

                    HospitalPatient patient = type switch
                    {
                        1 => new Outpatient(patientCounter, name, age, gender),
                        2 => new Inpatient(patientCounter, name, age, gender),
                        3 => new EmergencyPatient(patientCounter, name, age, gender),
                        _ => throw new Exception("Invalid type selected!")
                    };

                    system.RegisterPatient(patient);
                    patientCounter++;
                    break;

                case 2:
                    system.ShowPatients();
                    break;

                case 3:
                    Console.Write("Enter Patient ID to generate bill: ");
                    int id = int.Parse(Console.ReadLine());
                    HospitalPatient selected = system.FindPatientByID(id);

                    if (selected == null)
                    {
                        Console.WriteLine("Patient not found!");
                        break;
                    }

                    Console.WriteLine("Select billing strategy: 1. Standard  2. Emergency  3. Insurance");
                    int billType = int.Parse(Console.ReadLine());
                    BillingMethod method = billType switch
                    {
                        1 => billing.StandardBilling,
                        2 => billing.EmergencyBilling,
                        3 => billing.InsuranceBilling,
                        _ => billing.StandardBilling
                    };

                    system.ProcessBill(selected, method);
                    break;

                case 4:
                    Console.WriteLine("Exiting system. Goodbye!");
                    return;

                default:
                    Console.WriteLine("Invalid option! Try again.");
                    break;
            }
        }
    }
}
