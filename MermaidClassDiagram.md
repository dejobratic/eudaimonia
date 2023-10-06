```mermaid
classDiagram
    Year --|> ValueObject 
    Image --|> ValueObject 
    Text --|> ValueObject
    Rating --|> ValueObject

    class Year {
        int Value
    }

    class Image {
        Text Name
        string Url
        byte[] Data
        HasData => Data.Any()
    }

    class Rating {
        <<enumeration>>
        OneStar,
        TwoStar,
        ThreeStar,
        FourStar
        FiveStar
    }

    class Text {
        string Value
    }
    
    Book --* Edition
    Book --> Genre
    Book --> Author
    Book --> Publisher
    Book --* Reviews

    Review --> Book

    Author --|> User 
    Member --|> User

    Edition --> BookFormat

    Bookshelf --> User
    
    Bookshelf --* BookshelfBook

    BookshelfBook --> ReadingStatus
    BookshelfBook --> Book

    Rental --> BookshelfBook

    namespace Books {
        class Book {
            BookId Id
            Text Title
            Text Description
            Language Language
            UserId AuthorId
            Genre[] Genres
            Reviews Reviews
            Edition Edition
        }

        class Edition {
            uint PageCount
            Image FrontCover
            BookFormat Format
            PublisherId PublisherId
            Year PublicationYear
        }
  
        class BookFormat {
            <<enumeration>>
            Hardcover
            Paperback
            Audiobook
            Ebook
        }
  
        class Genre {
            <<enumeration>>
            ActionandAdventure,
            Classic,
            ContemporaryFiction,
            Dystopian,
            Fantasy,
            etc.
        }

        class Reviews {
            uint ReviewCount
            uint RatingCount
            uint FiveStarRatingCount
            uint FourStarRatingCount
            uint ThreeStarRatingCount
            uint TwoStarRatingCount
            uint OneStarRatingCount
        }

        class Author {
            BookId[] AuthoredBookIds
        }

        class Publisher {
            PublisherId Id
            Text Name
            BookId[] PublishedBookIds
        }        
    }
 
    namespace BookReviews {
        class Review {
            ReviewId Id
            BookId BookId
            UserId ReviewerId
            Rating Rating
            Comment Comment
        }
    }

    namespace Bookshelves {
        class Bookshelf {
            BookshelfId Id
            Text Name
            Text Description
            UserId OwnerId
            bool IsPublic
            BookshelfBook[] BookIds
        }

        class BookshelfBook {
            BookshelfBookId Id
            BookId BookId
            ReadingStatus Status
        }

        class ReadingStatus {
            <<enumeration>>
            WantToRead
            CurrentlyReading
            Read
        }
    }

    namespace BookRentals {
        class Rental {
            RentalId Id
            BookshelfBookId BookshelfBookId
            DateTime RentedAt
            Comments[] Comments
        }
    }

    class Member {
    }

    class User {
        UserId Id
        Text FulName
        Text Bio
        UserId[] FollowerIds
        UserId[] FollowingIds
    }

    class Comment {
        Text Text
        DateTime CreatedAt
        UserId CommenterId
        Comments[] Comments
    }   Rating Rating
            Comment Comment
        }
    }

    namespace Bookshelves {
        class Bookshelf {
            BookshelfId Id
            Text Name
            Text Description
            UserId OwnerId
            bool IsPublic
            BookshelfBook[] BookIds
        }

        class BookshelfBook {
            BookshelfBookId Id
            BookId BookId
            ReadingStatus Status
        }

        class ReadingStatus {
            <<enumeration>>
            WantToRead
            CurrentlyReading
            Read
        }
    }

    namespace BookRentals {
        class Rental {
            RentalId Id
            BookshelfBookId BookshelfBookId
            DateTime RentedAt
            Comments[] Comments
        }
    }

    class Member {
    }

    class User {
        UserId Id
        Text FulName
        Text Bio
        UserId[] FollowerIds
        UserId[] FollowingIds
    }

    class Comment {
        Text Text
        DateTime CreatedAt
        UserId CommenterId
        Comments[] Comments
    }
```