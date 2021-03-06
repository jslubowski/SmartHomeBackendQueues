// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SmartHome.DAL.Data;

namespace SmartHome.DAL.Migrations
{
    [DbContext(typeof(PostgresDbContext))]
    partial class PostgresDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("SmartHome.BLL.Entities.Enums.MeasurementTypeVariant", b =>
                {
                    b.Property<int>("MeasurementType")
                        .HasColumnType("integer")
                        .HasColumnName("measurement_type");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("MeasurementType")
                        .HasName("pk_measurement_type_variant");

                    b.ToTable("measurement_type_variant");

                    b.HasData(
                        new
                        {
                            MeasurementType = 1,
                            Name = "Temperature"
                        },
                        new
                        {
                            MeasurementType = 2,
                            Name = "Humidity"
                        });
                });

            modelBuilder.Entity("SmartHome.BLL.Entities.Enums.MeasurementUnitVariant", b =>
                {
                    b.Property<int>("MeasurementUnit")
                        .HasColumnType("integer")
                        .HasColumnName("measurement_unit");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("MeasurementUnit")
                        .HasName("pk_measurement_unit_variant");

                    b.ToTable("measurement_unit_variant");

                    b.HasData(
                        new
                        {
                            MeasurementUnit = 1,
                            Name = "Celsius"
                        },
                        new
                        {
                            MeasurementUnit = 2,
                            Name = "Fahrenheit"
                        },
                        new
                        {
                            MeasurementUnit = 3,
                            Name = "Kelvin"
                        },
                        new
                        {
                            MeasurementUnit = 4,
                            Name = "RH"
                        });
                });

            modelBuilder.Entity("SmartHome.BLL.Entities.Measurement", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date");

                    b.Property<Guid>("SensorId")
                        .HasColumnType("uuid")
                        .HasColumnName("sensor_id");

                    b.Property<float>("Value")
                        .HasColumnType("real")
                        .HasColumnName("value");

                    b.HasKey("Id")
                        .HasName("pk_measurement");

                    b.HasIndex("SensorId")
                        .HasDatabaseName("ix_measurement_sensor_id");

                    b.ToTable("measurement");
                });

            modelBuilder.Entity("SmartHome.BLL.Entities.Sensor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("CustomName")
                        .HasColumnType("text")
                        .HasColumnName("custom_name");

                    b.Property<float>("LatestValue")
                        .HasColumnType("real")
                        .HasColumnName("latest_value");

                    b.Property<float?>("LowerTriggerLimit")
                        .HasColumnType("real")
                        .HasColumnName("lower_trigger_limit");

                    b.Property<int>("MeasurementType")
                        .HasColumnType("integer")
                        .HasColumnName("measurement_type");

                    b.Property<int>("MeasurementUnit")
                        .HasColumnType("integer")
                        .HasColumnName("measurement_unit");

                    b.Property<float?>("UpperTriggerLimit")
                        .HasColumnType("real")
                        .HasColumnName("upper_trigger_limit");

                    b.HasKey("Id")
                        .HasName("pk_sensors");

                    b.ToTable("sensors");
                });

            modelBuilder.Entity("SmartHome.BLL.Entities.Measurement", b =>
                {
                    b.HasOne("SmartHome.BLL.Entities.Sensor", "Sensor")
                        .WithMany("Measurements")
                        .HasForeignKey("SensorId")
                        .HasConstraintName("fk_measurement_sensors_sensor_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sensor");
                });

            modelBuilder.Entity("SmartHome.BLL.Entities.Sensor", b =>
                {
                    b.Navigation("Measurements");
                });
#pragma warning restore 612, 618
        }
    }
}
