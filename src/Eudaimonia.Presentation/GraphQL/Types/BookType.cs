using Eudaimonia.Application.Utils.Dtos;
using GraphQL.Types;

namespace Eudaimonia.Presentation.GraphQL.Types;

public class BookType : ObjectGraphType<BookDto>
{
    public BookType()
    {
        Name = "Book";

        Field(_ => _.Id).Description("Book's id.");
        Field(_ => _.OriginalTitle).Description("Book's orignal title.");
        Field(_ => _.OriginalLanguage).Description("Book's original language.");
        Field(_ => _.AuthorId).Description("Book's author id.");
        Field<AuthorType>(nameof(BookDto.Author)).Description("Book's author.");
        Field(_ => _.DefaultEditionId).Description("Book's default edition id.");
        Field<EditionType>(nameof(BookDto.DefaultEdition)).Description("Book's default edition.");
        Field<ListGraphType<StringGraphType>>(nameof(BookDto.Genres)).Description("Book's genres.");
    }
}