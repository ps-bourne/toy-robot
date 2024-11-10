# toy-robot
## Features

- Place the robot to valid position
- Move the robot forward
- Turn the robot left or right
- Report the robot's current position

## Installation

1. Clone the repository:
    ```sh
    git clone https://github.com/ps-bourne/toy-robot.git
    ```
2. Navigate to the project directory:
    ```sh
    cd toy-robot
    ```
3. Install the dependencies:
    ```sh
    dotnet restore
    ```

## Usage

1. Start the application:
    ```sh
    dotnet run
    ```
2. Control the toy robot using following commands
    - PLACE X,Y,F
    - MOVE
    - LEFT
    - RIGHT
    - REPORT

## Running Tests

To run the tests, use the following command:
```sh
dotnet test
```

## Example Input and Output:
- a)----------------
```sh
PLACE 0,0,NORTH
MOVE
REPORT
Output: 0,1,NORTH
```
- b)----------------
```sh
PLACE 0,0,NORTH
LEFT
REPORT
Output: 0,0,WEST
```
- c)----------------
```sh
PLACE 1,2,EAST
MOVE
MOVE
LEFT
MOVE
REPORT
Output: 3,3,NORT
```