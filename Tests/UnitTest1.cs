using System.Collections.Generic;
using System.Threading;
using ClientServer.Commands.AlbumsCommands.Create;
using ClientServer.Requests.AlbumsRequests;
using ClientServer.Responses.AlbumsResponses;
using ClientServer.Validators.AlbumsRequestsValidators.Create;
using MassTransit;
using Moq;
using Moq.AutoMock;

namespace Tests;

public class CreateAlbumCommandTest
{
    private AutoMocker _autoMocker;
    private ICreateAlbumCommand _command;

    private CreateAlbumRequest _createAlbumRequest = new CreateAlbumRequest();
    private CreateAlbumRequest _createAlbumBadRequest = new CreateAlbumRequest();
    private CreateAlbumResponse _createAlbumResponse = new CreateAlbumResponse();
    private CreateAlbumResponse _badCreateAlbumRequestResponse = new CreateAlbumResponse();
    private Response<CreateAlbumResponse> _response;
    private string _errorMessage = "Error Message";
    
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _createAlbumRequest = new()
        {
            Name = "GoodName",
            Description = "GoodDescription"
        };

        _createAlbumBadRequest = new()
        {
            Name = "ReallyReallyBadName",
            Description = "ReallyReallyReallyBadDescription"
        };
        
        _createAlbumResponse = new CreateAlbumResponse()
        {
            StatusCode = true,
            Errors = new List<string>()
        };
        
        _badCreateAlbumRequestResponse = new CreateAlbumResponse()
        {
            StatusCode = false,
            Errors = new List<string>() { _errorMessage }
        };
    }
    
    [SetUp]
    public void Setup()
    {
        _autoMocker = new AutoMocker();
        _command = _autoMocker.CreateInstance<CreateAlbumCommand>();
        
        _autoMocker
            .Setup<ICreateAlbumValidator, bool>(x => x.ValidateAsync(_createAlbumRequest, default).Result.IsValid)
            .Returns(true);

        _autoMocker
            .Setup<IRequestClient<CreateAlbumRequest>, CreateAlbumResponse>(x =>
                x.GetResponse<CreateAlbumResponse>(_createAlbumRequest, CancellationToken.None, RequestTimeout.None).Result.Message)
            .Returns(_createAlbumResponse);
    }

    private void Verifiable(
        Times createAlbumValidatorTimes,
        Times createAlbumResponseTimes)
    {
        _autoMocker
            .Verify<ICreateAlbumValidator, bool>(x => 
                x.ValidateAsync(_createAlbumRequest, default).Result.IsValid, createAlbumValidatorTimes);

        _autoMocker
            .Verify<IRequestClient<CreateAlbumRequest>, CreateAlbumResponse>(x =>
                x.GetResponse<CreateAlbumResponse>(_createAlbumRequest, CancellationToken.None, RequestTimeout.None).Result.Message, createAlbumResponseTimes);
    }

    [Test]
    public void ExecuteTestSuccess()
    {
        CreateAlbumResponse goodResponse = new()
        {
            StatusCode = _createAlbumResponse.StatusCode,
            Errors = new List<string>()
        };
        Assert.AreEqual(_command.Execute(_createAlbumRequest).Result.StatusCode, goodResponse.StatusCode);
        Assert.AreEqual(_createAlbumResponse.Errors, goodResponse.Errors);
        
        Verifiable(
            createAlbumResponseTimes: Times.Once(),
            createAlbumValidatorTimes: Times.Once());
    }
    
    [Test]
    public void ExecuteWithNullualeRequestThenThrowsFailureResponse()
    {
        CreateAlbumResponse badResponse = new()
        {
            StatusCode = _badCreateAlbumRequestResponse.StatusCode
        };
        Assert.AreEqual(_command.Execute(null).Result.StatusCode, badResponse.StatusCode);

        Verifiable(
            createAlbumResponseTimes: Times.Never(),
            createAlbumValidatorTimes: Times.Never());
    }
    
    [Test]
    public void ExecuteWithFailedValidThenThrowsFailureResponse()
    {
        _autoMocker
            .Setup<ICreateAlbumValidator, bool>(x => x.ValidateAsync(_createAlbumRequest, default).Result.IsValid)
            .Returns(false);
        
        CreateAlbumResponse badResponse = new()
        {
            StatusCode = _badCreateAlbumRequestResponse.StatusCode
        };
        Assert.AreEqual(_command.Execute(_createAlbumRequest).Result.StatusCode, badResponse.StatusCode);

        Verifiable(
            createAlbumResponseTimes: Times.Never(),
            createAlbumValidatorTimes: Times.Once());
    }
}