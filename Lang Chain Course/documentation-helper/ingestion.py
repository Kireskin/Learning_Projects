import os
from dotenv import load_dotenv

from langchain_community.document_loaders import ReadTheDocsLoader
from langchain_openai import OpenAIEmbeddings
from langchain.text_splitter import RecursiveCharacterTextSplitter
from langchain_community.vectorstores import Pinecone as PineconeLangChain
from pinecone import Pinecone

load_dotenv()

pc = Pinecone(
    api_key=os.environ.get("PINECONE_API_KEY"),
)

INDEX_NAME = "langchain-docs-index"
original_path = "https://api.python.langchain.com/en/"


def ingest_docs():
    loader = ReadTheDocsLoader(
        "./langchain-docs/",
        encoding="utf-8"
    )

    raw_documents = loader.load()
    print(f"loaded {len(raw_documents)} documents")

    text_splitter = RecursiveCharacterTextSplitter(chunk_size=600, chunk_overlap=50)
    documents = text_splitter.split_documents(raw_documents)
    print(f"Splitted into {len(documents)} chunks")

    for doc in documents:
        old_path = doc.metadata["source"].replace('@', '/')
        new_url = old_path.replace("langchain-docs\\", original_path)
        doc.metadata.update({"source": new_url})
    print(f"Going to insert {len(documents)} to Pinecone")

    embeddings = OpenAIEmbeddings(model="text-embedding-3-small")
    PineconeLangChain.from_documents(documents, embeddings, index_name=INDEX_NAME)
    print("****Loading to vectorstore done ***")


if __name__ == "__main__":
    ingest_docs()
