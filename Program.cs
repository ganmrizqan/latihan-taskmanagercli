using System;
using SimpleTaskManager.Services;
using SimpleTaskManager.Models;
using System.Globalization;
using System.ComponentModel.Design;

class Program
{
    static TaskService service = new TaskService();

    static void Main(string[] args)
    {
        bool running = true;

        while (running)
        {
            Menu();
            Console.Write("Pilih menu (1-5): ");
            string opt = Console.ReadLine() ?? string.Empty;
            Console.WriteLine();

            switch (opt)
            {
                case "1":
                    ShowTasks();
                    break;
                case "2":
                    AddTask();
                    break;
                case "3":
                    MarkDone();
                    break;
                case "4":
                    DeleteTask();
                    break;
                case "5":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Pilihan tidak valid. Silakan coba lagi.");
                    break;
            }

            if (running)
            {
                Console.WriteLine("\nTekan Enter untuk melanjutkan...");
                Console.ReadLine();
            }
        }
    }

    static void Menu()
    {
        Console.Clear();
        Console.WriteLine("=== SIMPLE TASK MANAGER ===");
        Console.WriteLine("1. Lihat Semua Tugas");
        Console.WriteLine("2. Tambah Tugas Baru");
        Console.WriteLine("3. Tandai Tugas Selesai");
        Console.WriteLine("4. Hapus Tugas");
        Console.WriteLine("5. Keluar");
        Console.WriteLine();
    }

    static void ShowTasks()
    {
        var data = service.GetAll();
        if (data.Count == 0)
        {
            Console.WriteLine("Tidak ada tugas.");
            return;
        }

        Console.WriteLine("Daftar Tugas:");
        foreach (var t in data)
        {
            Console.WriteLine(t);
            Console.WriteLine();
        }
    }

    static void AddTask()
    {
        Console.Write("Judul Tugas: ");
        string title = Console.ReadLine() ?? string.Empty;

        Console.Write("Deskripsi Tugas: ");
        string description = Console.ReadLine() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(description))
        {
            Console.WriteLine("Judul dan deskripsi tugas tidak boleh kosong.");
            return;
        }

        Console.Write("Tanggal Jatuh Tempo (dd-MM-yyyy) [Opsional]: ");
        string dueDateInput = Console.ReadLine() ?? string.Empty;

        DateTime? due = null;
        if (!string.IsNullOrWhiteSpace(dueDateInput))
        {
            if (DateTime.TryParseExact(dueDateInput, "dd-MM-yyyy", null, DateTimeStyles.None, out DateTime parsed))
            {
                due = parsed;
            }
        }

        service.AddTask(title, description, due);
        Console.WriteLine("Tugas berhasil ditambahkan.");
    }

    static void MarkDone()
    {
        Console.Write("Masukkan ID Tugas yang sudah selesai: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            if (service.MarkDone(id))
            {
                Console.WriteLine("Tugas berhasil ditandai sebagai selesai.");
            }
            else
            {
                Console.WriteLine("Tugas dengan ID tersebut tidak ditemukan.");
            }
        }
        else
        {
            Console.WriteLine("ID tidak valid.");
        }
    }

    static void DeleteTask()
    {
        Console.Write("Masukkan ID Tugas yang akan dihapus: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            if (service.DeleteTask(id))
            {
                Console.WriteLine("Tugas berhasil dihapus.");
            }
            else
            {
                Console.WriteLine("Tugas dengan ID tersebut tidak ditemukan.");
            }
        }
        else
        {
            Console.WriteLine("ID tidak valid.");
        }
    }
}