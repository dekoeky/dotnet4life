using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace WebApplication1.Tests;

public class BasicTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    [Theory]
    [InlineData("/MinimalApis/TimeOfDay")]
    [InlineData("/MinimalApis/Timestamp")]
    [InlineData("/MinimalApis/Date")]
    [InlineData("/MinimalApis/WhatDayIsIt")]
    [InlineData("/MinimalApis/WhatDayIsIt/Json")]
    [InlineData("/MinimalApis/ResultsDemo/typed/ok")]
    [InlineData("/MinimalApis/ResultsDemo/typed/notfound", HttpStatusCode.NotFound)]
    [InlineData("/MinimalApis/ResultsDemo/regular/ok")]
    [InlineData("/MinimalApis/ResultsDemo/regular/notfound", HttpStatusCode.NotFound)]
    [InlineData("/MinimalApis/WeatherForecast")]
    public async Task Get_EndpointsReturnCorrectStatusCodes(string url, HttpStatusCode? code = null)
    {
        // Arrange
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync(url);

        // Assert
        if (code is null)
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        else
            Assert.Equal(code, response.StatusCode);
    }
}