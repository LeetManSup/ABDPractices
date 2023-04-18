from fastapi import FastAPI
from app.server.routers.employee_router import router as employee
from app.server.routers.speaker_router import router as speaker
from app.server.databases.posgres import metadata, postgres_db, engine

app = FastAPI()
app.include_router(employee, tags=['Employee'], prefix='/employee')
app.include_router(speaker, tags=['Speaker'], prefix='/speaker')
metadata.create_all(engine)


@app.on_event('startup')
async def startup():
    await postgres_db.connect()


@app.on_event('shutdown')
async def shutdown():
    await postgres_db.disconnect()

