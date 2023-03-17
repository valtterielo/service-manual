<?xml version="1.0" encoding="UTF-8" ?>
# Service Manual

Service Manual is a .net rest api project aiming to simulate a real world scenario where users need an api solution for tracking maintenance tasks. The project consists of 2 tables that have a many to one relationship. Each maintenance task has one assigned factory device. Factory devices can have multiple maintenance tasks associated with them.

## Installation

```bash
git clone https://github.com/valtterielo/service-manual
```

## Usage

```python
FACTORY DEVICES

[GET] /api/FactoryDevices {returns a list of factory devices} 

[POST] /api/FactoryDevices {post new factory device to the db}

[PUT] /api/FactoryDevices {modify existing factory device}

[DELETE] /api/FactoryDevices {delete a factory device from the db}

[GET] /api/FactoryDevices/{id} {get a spesific factory device returned from the db}

--------------------------------------------------------------------------------------
MAINTENANCE TASKS

[GET] /api/MaintenanceTasks {returns a list of maintenance tasks}

[POST] /api/MaintenanceTasks {post new maintenance to the db}

[PUT] /api/MaintenanceTasks {modify existing maintenance task}

[DELETE] /api/MaintenanceTasks {delete a maintenance task from the db}

[GET] /api/MaintenanceTasks/{id} {get a spesific maintenance task returned from the db}

[GET] /api/MaintenanceTasks/Filter/deviceId {return a list of maintenance tasks filtered by a factory device}
```

