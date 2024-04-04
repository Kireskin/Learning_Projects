from dotenv import load_dotenv
from langchain import hub
from langchain.agents import AgentType, create_openai_functions_agent, AgentExecutor
from langchain.tools.render import render_text_description
from langchain_core.prompts import ChatPromptTemplate
from langchain_core.tools import Tool
from langchain_experimental.agents import create_csv_agent
from langchain_experimental.agents.agent_toolkits import create_python_agent
from langchain_openai import ChatOpenAI
from langchain_experimental.tools import PythonREPLTool

load_dotenv()


def main():
    print("Start...")
    python_agent_executor = create_python_agent(
        llm=ChatOpenAI(temperature=0, model="gpt-3.5-turbo"),
        tool=PythonREPLTool(),
        agent_type=AgentType.ZERO_SHOT_REACT_DESCRIPTION,
        verbose=True,
    )

    # python_agent_executor.invoke(
    #     {"input": """generate and save in current working directory 15 QRcodes
    #                             that point to www.udemy.com/course/langchain, you have qrcode package installed already"""}
    # )

    csv_agent = create_csv_agent(
        llm=ChatOpenAI(temperature=0, model="gpt-3.5-turbo"),
        path="episode_info.csv",
        verbose=True,
        agent_type=AgentType.ZERO_SHOT_REACT_DESCRIPTION,
    )

    # csv_agent.invoke({"input": "how many columns are there in file episode_info.csv"})
    # csv_agent.invoke({"input": "which writer wrote the most episodes? how many did he write? be aware that some episodes were writen by multiple "
    #                            "writers and you have to split them"})

    prompt = hub.pull("hwchase17/openai-functions-agent")
    # print(prompt.messages)

    tools = [
        Tool(
            name="PythonAgent",
            func=python_agent_executor.invoke,
            description="""useful when you need to transform natural language and write from it python and execute the python code,
                                  returning the results of the code execution,
                                DO NOT SEND PYTHON CODE TO THIS TOOL""",
        ),
        Tool(
            name="CSVAgent",
            func=csv_agent.invoke,
            description="""useful when you need to answer question over episode_info.csv file,
                                 takes an input the entire question and returns the answer after running pandas calculations""",
        ),
    ]

    grand_agent = create_openai_functions_agent(
        tools=tools,
        llm=ChatOpenAI(temperature=0, model="gpt-3.5-turbo"),
        prompt=prompt
    )

    grand_agent_executor = AgentExecutor(agent=grand_agent, tools=tools, verbose=True)

    grand_agent_executor.invoke({"input": "print seasons ascending order of the number of episodes they have"})


if __name__ == "__main__":
    main()
