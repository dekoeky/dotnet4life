using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.IO.Compression;
using System.Text.Json;
using WebApplication1.Database.Models;
using WebApplication1.Services;
using Color = SixLabors.ImageSharp.Color;

namespace WebApplication1.MinimalApis.Export;

internal static class DownloadMinimalApis
{
    private static async Task GenerateImageForRowAsync(Row row, Stream output, CancellationToken ct)
    {

        var image = new Image<Argb32>(400, 100, new Argb32(255, 255, 255));

        var font = SystemFonts.CreateFont("Arial", 24);
        var options = new TextOptions(font)
        {
            Origin = new System.Numerics.Vector2(10, 40)
        };
        var brush = new SolidBrush(Color.Black);

        image.Mutate(ctx =>
        {
            ctx.DrawText($"Row {row.Id}", font, brush, new PointF(10, 40));
        });

        await image.SaveAsPngAsync(output, ct);
    }


    extension(IEndpointRouteBuilder r)
    {
        public void MapDownloadDemonstrationEndpoints()
        {
            r.SimpleDemo();
        }

        private void SimpleDemo()
        {
            const int defaultN = 10000;
            static Row CreateRow(Random rnd, int i) => new(i, $"Value{i}", rnd.NextDouble());

            r.MapGet("/download/csv", async (HttpResponse response, AsyncDataGenerator db, int n = defaultN, CancellationToken ct = default) =>
            {
                response.Headers.ContentType = "text/csv";
                response.Headers.ContentDisposition = "attachment; filename=\"data.csv\"";

                await using var writer = new StreamWriter(response.Body);

                await writer.WriteLineAsync("Id,Name,Value");

                await foreach (var row in db.GenerateAsync(CreateRow, n).WithCancellation(ct))
                {
                    await writer.WriteLineAsync($"{row.Id},{row.Name},{row.Value}");
                    await writer.FlushAsync(ct);
                }
            });

            r.MapGet("/download/json", async (HttpResponse response, AsyncDataGenerator db, int n = defaultN, CancellationToken ct = default) =>
            {
                response.Headers.ContentType = "application/json";
                response.Headers.ContentDisposition = "attachment; filename=\"data.json\"";

                await using var json = new Utf8JsonWriter(response.Body);

                json.WriteStartArray();

                await foreach (var row in db.GenerateAsync(CreateRow, n).WithCancellation(ct))
                {
                    json.WriteStartObject();
                    json.WriteNumber("id", row.Id);
                    json.WriteString("name", row.Name);
                    json.WriteNumber("value", row.Value);
                    json.WriteEndObject();

                    await json.FlushAsync(ct);
                }

                json.WriteEndArray();
                await json.FlushAsync(ct);
            });

            r.MapGet("/download/zip", async (HttpResponse response, AsyncDataGenerator db, int n = defaultN, CancellationToken ct = default) =>
            {
                response.Headers.ContentType = "application/zip";
                response.Headers.ContentDisposition = "attachment; filename=\"data.zip\"";

                using var zip = new ZipArchive(response.Body, ZipArchiveMode.Create, leaveOpen: true);
                var entry = zip.CreateEntry("data.csv");

                await using var entryStream = entry.Open();
                await using var writer = new StreamWriter(entryStream);

                await writer.WriteLineAsync("Id,Name,Value");

                await foreach (var row in db.GenerateAsync(CreateRow, n).WithCancellation(ct))
                {
                    await writer.WriteLineAsync($"{row.Id},{row.Name},{row.Value}");
                    await writer.FlushAsync(ct);
                }
            });

            r.MapGet("/download/zip2", async (
                HttpResponse response,
                AsyncDataGenerator db,
                int n = defaultN,
                CancellationToken ct = default) =>
            {
                await response.StartAsync(ct);

                await using var zipStream = response.BodyWriter.AsStream();
                using var zip = new ZipArchive(zipStream, ZipArchiveMode.Create, leaveOpen: true);

                var counter = 0;

                await foreach (var row in db.GenerateAsync(CreateRow, n).WithCancellation(ct))
                {
                    string imageName = $"row_{row.Id}.png";

                    // 2. Create image entry (must be fully closed before next)
                    var imgEntry = zip.CreateEntry(imageName, CompressionLevel.Fastest);
                    await using (var imgStream = imgEntry.Open())
                    {
                        await GenerateImageForRowAsync(row, imgStream, ct);
                    }

                    if (++counter % 100 == 0)
                        await response.BodyWriter.FlushAsync(ct);
                }

                await response.BodyWriter.FlushAsync(ct);



                //response.Headers.ContentType = "application/zip";
                //response.Headers.ContentDisposition = "attachment; filename=\"export.zip\"";

                //await response.StartAsync(ct); // send headers immediately

                ////using var zip = new ZipArchive(response.Body, ZipArchiveMode.Create, leaveOpen: true);
                //using var zip = new ZipArchive(response.BodyWriter.AsStream(), ZipArchiveMode.Create, leaveOpen: true);

                //// --- 1. Create CSV entry ---
                //var csvEntry = zip.CreateEntry("data.csv", CompressionLevel.Fastest);
                //await using var csvStream = csvEntry.Open();
                //await using var csvWriter = new StreamWriter(csvStream);

                //await csvWriter.WriteLineAsync("Id,Name,Value,ImageFile");

                //int counter = 0;

                //await foreach (var row in db.GenerateAsync(CreateRow, n).WithCancellation(ct))
                //{
                //    string imageName = $"row_{row.Id}.png";

                //    // Write CSV row
                //    await csvWriter.WriteLineAsync($"{row.Id},{row.Name},{row.Value},{imageName}");

                //    // --- 2. Create image entry for this row ---
                //    var imgEntry = zip.CreateEntry(imageName, CompressionLevel.Fastest);
                //    await using var imgStream = imgEntry.Open();

                //    // Generate image bytes (or stream) for this row
                //    await GenerateImageForRowAsync(row, imgStream, ct);

                //    // Optional: flush ZIP every X rows
                //    if (++counter % 100 == 0)
                //        await response.Body.FlushAsync(ct);
                //}

                //await csvWriter.FlushAsync(ct);
            });





        }
    }
}