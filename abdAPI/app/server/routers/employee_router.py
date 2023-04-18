from typing import List
from fastapi import APIRouter, HTTPException
from app.server.models.employee_model import EmployeeIn, EmployeeOut, EmployeeUpdate
from app.server.databases.posgres import employee, postgres_db

router = APIRouter()


@router.get('/', response_model=List[EmployeeOut])
async def index():
    query = employee.select()

    return await postgres_db.fetch_all(query=query)


@router.post('/', status_code=201)
async def add_employee(payload: EmployeeIn):
    query = employee.insert().values(**payload.dict())
    employee_id = await postgres_db.execute(query=query)
    response = {
        'id': employee_id,
        **payload.dict()
    }

    return response


@router.put('/{id}')
async def update_employee(id: int, payload: EmployeeUpdate):
    query = employee.select(employee.c.id == id)
    cur_employee = await postgres_db.fetch_one(query=query)
    if not cur_employee:
        raise HTTPException(status_code=404, detail='Сотрудник с указанным ID не найден')

    update_data = payload.dict(exclude_unset=True)
    employee_in_db = EmployeeIn(**cur_employee)

    updated_employee = employee_in_db.copy(update=update_data)

    query = (
        employee
        .update()
        .where(employee.c.id == id)
        .values(**updated_employee.dict())
    )

    return await postgres_db.execute(query=query)


@router.delete('/{id}')
async def delete_employee(id: int):
    query = employee.select(employee.c.id == id)
    cur_employee = await postgres_db.fetch_one(query=query)
    if not cur_employee:
        raise HTTPException(status_code=404, detail='Сотрудник с указанным ID не найден')

    query = employee.delete().where(employee.c.id == id)

    return await postgres_db.execute(query=query)
