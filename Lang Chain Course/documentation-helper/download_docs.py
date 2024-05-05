import requests
from bs4 import BeautifulSoup
import os
import urllib
import re

# The URL to scrape
url = "https://api.python.langchain.com/en/latest/langchain_api_reference.html"
remove = "https://api.python.langchain.com/en/"


def remove_url_part(url, part_to_remove):
    # Escape special characters in the part_to_remove
    escaped_part = re.escape(part_to_remove)
    # Create a regex pattern to match the part_to_remove
    pattern = re.compile(escaped_part)
    # Replace the part_to_remove with an empty string
    cleaned_url = re.sub(pattern, '', url)
    return cleaned_url


# The directory to store files in
output_dir = "./langchain-docs/"
 
# Create the output directory if it doesn't exist
os.makedirs(output_dir, exist_ok=True)
 
# Fetch the page
response = requests.get(url)
soup = BeautifulSoup(response.text, 'html.parser')
 
# Find all links to .html files
links = soup.find_all('a', href=True)
 
for link in links:
    href = link['href'].split('#')[0]
    
    # If it's a .html file
    if href.endswith('.html'):
        # Make a full URL if necessary
        if not href.startswith('http'):
            href = urllib.parse.urljoin(url, href)
            print(href)
            
        # Fetch the .html file
        file_response = requests.get(href)
        
        # Write it to a file
        file_name = os.path.join(output_dir, remove_url_part(href, remove).replace('/', '@'))
        with open(file_name, 'w', encoding='utf-8') as file:
            file.write(file_response.text)
