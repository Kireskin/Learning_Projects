import os

from langchain_community.document_loaders import PyPDFLoader
from langchain.text_splitter import CharacterTextSplitter
from langchain_openai import OpenAIEmbeddings
from langchain_community.vectorstores import FAISS
from langchain.chains import RetrievalQA
from langchain_openai import OpenAI

os.environ["OPENAI_API_KEY"] = "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"

if __name__ == "__main__":
    print("hi")
    pdf_path = "D:/Projects/vector-store-in-memory/2210.03629.pdf"
    loader = PyPDFLoader(file_path=pdf_path)
    documents = loader.load()
    text_splitter = CharacterTextSplitter(
        chunk_size=1000, chunk_overlap=30, separator="\n"
    )
    docs = text_splitter.split_documents(documents=documents)

    embeddings = OpenAIEmbeddings()
    vectorstore = FAISS.from_documents(docs, embeddings)
    vectorstore.save_local("faiss_index_react")

    new_vectorstore = FAISS.load_local("faiss_index_react", embeddings, allow_dangerous_deserialization=True)
    qa = RetrievalQA.from_chain_type(
        llm=OpenAI(), chain_type="stuff", retriever=new_vectorstore.as_retriever()
    )
    query = "Give me the gist of ReAct in 3 sentences."
    res = qa.invoke({"query": query})
    print(res)
