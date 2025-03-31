"use client";

import { useState } from "react";

const Profile = () => {
    // const { user } = useUser();

    const [currState, setCurrState] = useState<"auth" | "login">("auth");

    const [user, setUser] = useState<any>({
        name: "",
        age: 0,
        town: "",
        username: "",
        email: "",
        password: "",
    });

    const auth = async () => {
        const res = await fetch("/api/auth", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(user),
        });

        if (res.status === 200) {
            alert("Авторизация успешна");
        } else {
            alert("Ошибка авторизации");
        }
    };

    const login = async () => {
        const res = await fetch("http://localhost:5001/api/v1/login", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(user),
        });

        if (res.status === 200) {
            alert("Авторизация успешна");
        } else {
            alert("Ошибка авторизации");
        }
    };

    return (
        <div className="pt-5 flex flex-col w-full items-center gap-2">
            <p className="text-2xl font-bold">Ваш профиль</p>
            <div className="flex flex-col gap-3">
                {currState === "auth" ? (
                    <>
                        <input
                            value={user.name}
                            onChange={(e) =>
                                setUser({ ...user, name: e.target.value })
                            }
                            type="text"
                            placeholder="Введите имя"
                        />
                        <input
                            value={user.age}
                            onChange={(e) =>
                                setUser({ ...user, age: e.target.value })
                            }
                            type="number"
                            placeholder="Введите возраст"
                        />
                        <input
                            value={user.town}
                            onChange={(e) =>
                                setUser({ ...user, town: e.target.value })
                            }
                            type="text"
                            placeholder="Введите город"
                        />
                        <input
                            value={user.username}
                            onChange={(e) =>
                                setUser({ ...user, username: e.target.value })
                            }
                            type="text"
                            placeholder="Введите логин"
                        />
                        <input
                            value={user.email}
                            onChange={(e) =>
                                setUser({ ...user, email: e.target.value })
                            }
                            type="text"
                            placeholder="Введите email"
                        />
                        <input
                            value={user.password}
                            onChange={(e) =>
                                setUser({ ...user, password: e.target.value })
                            }
                            type="password"
                            placeholder="Введите пароль"
                        />
                        <button className="cursor-pointer">Зарегаться</button>
                    </>
                ) : (
                    <>
                        <input
                            value={user.username}
                            onChange={(e) =>
                                setUser({ ...user, username: e.target.value })
                            }
                            type="text"
                            placeholder="Введите логин"
                        />
                        <input
                            value={user.password}
                            onChange={(e) =>
                                setUser({ ...user, password: e.target.value })
                            }
                            type="password"
                            placeholder="Введите пароль"
                        />
                        <button className="cursor-pointer">Войти</button>
                    </>
                )}

                {currState === "auth" ? (
                    <button
                        onClick={() => setCurrState("login")}
                        className="cursor-pointer">
                        Есть акк? Дык входи
                    </button>
                ) : (
                    <button
                        onClick={() => setCurrState("auth")}
                        className="cursor-pointer">
                        Нет акка? Дык создай
                    </button>
                )}
            </div>
        </div>
    );
};

export default Profile;
