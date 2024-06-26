import gql from 'graphql-tag';

export const CREATE_BOOK = gql`
  mutation CreateBook($request: CreateBookRequestInput!) {
    createBook(request: $request) {
      id
      title
      author
      publishedDate
      isbn
    }
  }
`;

export const UPDATE_BOOK = gql`
  mutation updateBook($request: UpdateBookRequestInput!) {
    updateBook(
      request: $request
    ) {
      author
      id
      isbn
      publishedDate
      title
    }
  }
`;

export const DELETE_BOOK = gql`
  mutation DeleteBook($id: String!) {
    deleteBook(id: $id)
  }
`;
