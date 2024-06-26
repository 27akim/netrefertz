import gql from 'graphql-tag';

export const GET_BOOK_BY_ID = gql`
  query GetBookById($id: String!) {
    bookById(id: $id) {
      id
      title
      author
      publishedDate
      isbn
    }
  }
`;

export const GET_BOOKS = gql`
  query {
    books {
      id
      title
      author
      publishedDate
      isbn
    }
  }
`;
