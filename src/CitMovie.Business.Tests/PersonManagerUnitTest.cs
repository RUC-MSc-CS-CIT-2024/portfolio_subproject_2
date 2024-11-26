using CitMovie.Data;
using CitMovie.Models.DomainObjects;
using FakeItEasy;
using AutoMapper;
using CitMovie.Models.DataTransferObjects;

namespace CitMovie.Business.Tests;

public class PersonManagerTest
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;
    private readonly PersonManager _personManager;

    public PersonManagerTest()
    {
        _personRepository = A.Fake<IPersonRepository>();
        _mapper = A.Fake<IMapper>();
        _personManager = new PersonManager(_personRepository, _mapper);
    }

    [Fact]
    public async Task GetPersonsAsync_ValidParameters_CallsRepositoryAndMapsResult()
    {
        // Arrange
        var persons = new List<Person> { new Person() { Name = A.Dummy<string>() } };
        A.CallTo(() => _personRepository.GetPersonsAsync(1, 10)).Returns(persons);
        A.CallTo(() => _mapper.Map<IEnumerable<PersonResult>>(persons)).Returns(new List<PersonResult>());

        // Act
        var result = await _personManager.GetPersonsAsync(1, 10);

        // Assert
        A.CallTo(() => _personRepository.GetPersonsAsync(1, 10)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _mapper.Map<IEnumerable<PersonResult>>(persons)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task GetTotalPersonsCountAsync_ValidCall_CallsRepository()
    {
        // Arrange
        A.CallTo(() => _personRepository.GetTotalPersonsCountAsync()).Returns(100);

        // Act
        var result = await _personManager.GetTotalPersonsCountAsync();

        // Assert
        A.CallTo(() => _personRepository.GetTotalPersonsCountAsync()).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task GetPersonByIdAsync_ValidId_CallsRepositoryAndMapsResult()
    {
        // Arrange
        var person = new Person() { Name = A.Dummy<string>() };
        A.CallTo(() => _personRepository.GetPersonByIdAsync(1)).Returns(person);
        A.CallTo(() => _mapper.Map<PersonResult>(person)).Returns(new PersonResult() { Id = 1, Name = A.Dummy<string>() });

        // Act
        var result = await _personManager.GetPersonByIdAsync(1);

        // Assert
        A.CallTo(() => _personRepository.GetPersonByIdAsync(1)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _mapper.Map<PersonResult>(person)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task GetPersonByIdAsync_PersonNotFound_ReturnsNull()
    {
        // Arrange
        A.CallTo(() => _personRepository.GetPersonByIdAsync(1)).Returns((Person)null!);

        // Act
        var result = await _personManager.GetPersonByIdAsync(1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetMediaByPersonIdAsync_ValidParameters_CallsRepositoryAndMapsResult()
    {
        // Arrange
        var media = new List<Media>();
        A.CallTo(() => _personRepository.GetMediaByPersonIdAsync(1, 1, 10)).Returns(media);
        A.CallTo(() => _mapper.Map<IEnumerable<MediaBasicResult>>(media)).Returns(new List<MediaBasicResult>());

        // Act
        var result = await _personManager.GetMediaByPersonIdAsync(1, 1, 10);

        // Assert
        A.CallTo(() => _personRepository.GetMediaByPersonIdAsync(1, 1, 10)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _mapper.Map<IEnumerable<MediaBasicResult>>(media)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task GetMediaByPersonIdCountAsync_ValidCall_CallsRepository()
    {
        // Arrange
        A.CallTo(() => _personRepository.GetMediaByPersonIdCountAsync(1)).Returns(100);

        // Act
        var result = await _personManager.GetMediaByPersonIdCountAsync(1);

        // Assert
        A.CallTo(() => _personRepository.GetMediaByPersonIdCountAsync(1)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task GetFrequentCoActorsAsync_ValidParameters_CallsRepositoryAndMapsResult()
    {
        // Arrange
        var coActors = new List<CoActor>();
        A.CallTo(() => _personRepository.GetFrequentCoActorsAsync(1, 1, 10)).Returns(coActors);
        A.CallTo(() => _mapper.Map<IEnumerable<CoActorResult>>(coActors)).Returns(new List<CoActorResult>());

        // Act
        var result = await _personManager.GetFrequentCoActorsAsync(1, 1, 10);

        // Assert
        A.CallTo(() => _personRepository.GetFrequentCoActorsAsync(1, 1, 10)).MustHaveHappenedOnceExactly();
    }
}