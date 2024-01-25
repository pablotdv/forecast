# Weather forecast app

## Description

This is a simple weather forecast app that uses the `US National Weather Service API` to get the weather forecast for the next 7 days.

## Prerequisites

- Docker 

## Installation

1. Clone the repo
```sh
git clone git@github.com:pablotdv/forecast.git
```
2. build the images
```sh
docker-compose build --no-cache
```

## Usage
1. Run the containers
```sh
docker-compose up -d
```
2. Open your browser and go to `http://localhost:3000/`
3. Enter a valid Street, City, State, US zip code and click on the `SEARCH` button
4. The weather forecast for the next 7 days will be displayed
5. Enjoy!

## sample data
| Street | City | State | Zip Code |
| ------ | ---- | ----- | -------- |
| 4564 N Isle Royale St | Moorpark | California (CA) | 93021 |
| 242 W Hudson St | Long Beach | New York (NY) | 95014 |
| 6185 Ward Rd | Sanborn | New York (NY) | 14132 |

## improvements

[] handling errors on integrations with external services

[] add unit tests

[] add integration tests
