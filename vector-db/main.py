import os

from langchain_community.document_loaders import TextLoader
from langchain.text_splitter import CharacterTextSplitter
from langchain_openai import OpenAIEmbeddings
from langchain_community.vectorstores import Pinecone
from langchain.chains import RetrievalQA
from langchain_openai import OpenAI

os.environ["OPENAI_API_KEY"] = "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"
os.environ['PINECONE_API_KEY'] = "a4de6eba-144a-4fc5-a55c-bb9bdcecd563"

if __name__ == "__main__":
    print("hi")
    file_path = "D:/Projects/vector-db/What_is_a_Vector_Database.txt"
    loader = TextLoader(file_path=file_path)
    documents = loader.load()
    text_splitter = CharacterTextSplitter(
        chunk_size=1000, chunk_overlap=0
    )
    docs = text_splitter.split_documents(documents=documents)

    embeddings = OpenAIEmbeddings()
    vectorstore = Pinecone.from_documents(docs, embeddings, index_name="medium-blogs-embeddings-index")
    qa = RetrievalQA.from_chain_type(
        llm=OpenAI(), chain_type="stuff", retriever=vectorstore.as_retriever()
    )
    query = "What is a vector database? Give me a 15 word answer for beginner."
    res = qa.invoke({"query": query})
    print(res)
