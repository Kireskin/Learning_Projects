import re

from dotenv import load_dotenv
from langchain_core.prompts import PromptTemplate

from langchain import hub
from langchain.agents import create_react_agent, AgentExecutor
from langchain_core.tools import Tool
from langchain_openai import ChatOpenAI

from tools.tools import get_profile_url

load_dotenv()


def extract_twitter_username(url):
    # Regular expression pattern to match Twitter URLs
    pattern = r"https?://(?:www\.)?twitter\.com/(?:\w{2}/)?([a-zA-Z0-9_]+)(?:\?.*)?$"

    # Search for the username in the URL using the pattern
    match = re.search(pattern, url)

    if match:
        return match.group(1)  # Return the matched username
    else:
        return None  # Return None if no username is found


def lookup(name: str) -> str:
    llm = ChatOpenAI(temperature=0, model_name="gpt-3.5-turbo")
    template = """
       given the name {name_of_person} I want you to find a link to their Twitter profile page, and extract from it their username
       In Your Final answer only the person's username"""
    tools_for_agent_twitter = [
        Tool(
            name="Crawl Google 4 Twitter profile page",
            func=get_profile_url,
            description="useful for when you need get the Twitter Page URL",
        ),
        Tool(
            name="Extract twitter username from twitter profile page",
            func=extract_twitter_username,
            description="uses regular expression to find username in twitter url",
        ),
    ]

    # agent = initialize_agent(
    #     tools_for_agent_twitter,
    #     llm,
    #     agent=AgentType.ZERO_SHOT_REACT_DESCRIPTION,
    #     verbose=True,
    # )

    prompt_template = PromptTemplate(
        input_variables=["name_of_person"], template=template
    )

    react_prompt = hub.pull("hwchase17/react")
    agent = create_react_agent(
        llm=llm, tools=tools_for_agent_twitter, prompt=react_prompt
    )
    agent_executor = AgentExecutor(
        agent=agent, tools=tools_for_agent_twitter, verbose=True
    )

    result = agent_executor.invoke(
        input={"input": prompt_template.format_prompt(name_of_person=name)}
    )

    twitter_username = result["output"]

    return twitter_username
