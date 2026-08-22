using Hospital.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Hospital.Repositories
{
    public class ApplicationDbContext
        : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
     DbContextOptions<ApplicationDbContext> options)
     : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Bill> Bills { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<HospitalInfo> HospitalInfos { get; set; }

        public DbSet<Insurance> Insurances { get; set; }

        public DbSet<Lab> Labs { get; set; }

        public DbSet<Medicine> Medicines { get; set; }

        public DbSet<MedicineReport> MedicineReports { get; set; }

        public DbSet<PayRoll> Payrolls { get; set; }

        public DbSet<PatientReport> PatientReports { get; set; }

        public DbSet<PrescribedMedicine> PrescribedMedicines { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<TestPrice> TestPrices { get; set; }

        public DbSet<Timing> Timing { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<DoctorNote> DoctorNotes { get; set; }

        public DbSet<MedicalFile> MedicalFiles { get; set; }

        public DbSet<PatientInsurance> PatientInsurances { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<PatientReport>()
                .HasOne(p => p.Doctor)
                .WithMany()
                .HasForeignKey(p => p.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<PatientReport>()
                .HasOne(p => p.Patient)
                .WithMany()
                .HasForeignKey(p => p.PatientId)
                .OnDelete(DeleteBehavior.NoAction);



            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany()
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany()
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.NoAction);



            modelBuilder.Entity<Timing>()
                .HasOne(x => x.Doctor)
                .WithMany()
                .HasForeignKey(x => x.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);



            modelBuilder.Entity<Lab>()
                .HasOne(x => x.Patient)
                .WithMany()
                .HasForeignKey(x => x.PatientId)
                .OnDelete(DeleteBehavior.NoAction);



            modelBuilder.Entity<Bill>()
                .HasOne(x => x.Patient)
                .WithMany()
                .HasForeignKey(x => x.PatientId)
                .OnDelete(DeleteBehavior.NoAction);



            modelBuilder.Entity<Bill>()
                .HasOne(x => x.Doctor)
                .WithMany()
                .HasForeignKey(x => x.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);



            modelBuilder.Entity<Bill>()
                .HasOne(x => x.Appointment)
                .WithMany()
                .HasForeignKey(x => x.AppointmentId)
                .OnDelete(DeleteBehavior.NoAction);



            modelBuilder.Entity<PayRoll>()
                .HasOne(x => x.Employee)
                .WithMany()
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Medicine>()
                .HasOne(x => x.Supplier)
                .WithMany(x => x.Medicines)
                .HasForeignKey(x => x.SupplierId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MedicalFile>()

    .HasOne(x => x.Patient)

    .WithMany()

    .HasForeignKey(x => x.PatientId)

    .OnDelete(DeleteBehavior.NoAction);




            modelBuilder.Entity<MedicalFile>()

                .HasOne(x => x.Doctor)

                .WithMany()

                .HasForeignKey(x => x.DoctorId)

                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PatientInsurance>()

    .HasOne(x => x.Patient)

    .WithMany(x => x.PatientInsurances)

    .HasForeignKey(x => x.PatientId)

    .OnDelete(DeleteBehavior.NoAction);




            modelBuilder.Entity<PatientInsurance>()

                .HasOne(x => x.Insurance)

                .WithMany(x => x.PatientInsurances)

                .HasForeignKey(x => x.InsuranceId)

                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Room>()
    .HasOne(r => r.Patient)
    .WithOne(p => p.Room)
    .HasForeignKey<Room>(r => r.PatientId)
    .OnDelete(DeleteBehavior.SetNull);

        }
    }
}