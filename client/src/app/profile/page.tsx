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

    const [isEntered, setIsEntered] = useState(false);
    const [isLoading, setIsLoading] = useState(false);

    const TEMP_func = () => {
        setIsLoading(true);
        setTimeout(() => {
            setIsLoading(false);
            setIsEntered(true);
        }, 500);
    };

    if (isEntered) {
        return <div className="text-2xl mt-3 text-center">Вы вошли)</div>;
    }

    return (
        <div className="pt-5 flex flex-col w-full items-center gap-2">
            <p className="text-2xl font-bold">Ваш профиль</p>
            <div className="flex flex-col gap-3">
                <input
                    value={user.username}
                    onChange={(e) =>
                        setUser({ ...user, username: e.target.value })
                    }
                    type="text"
                    placeholder="Введите почту"
                />
                <input
                    value={user.password}
                    onChange={(e) =>
                        setUser({ ...user, password: e.target.value })
                    }
                    type="password"
                    placeholder="Введите пароль"
                />
                <button
                    disabled={isLoading}
                    onClick={() => TEMP_func()}
                    className="cursor-pointer">
                    {currState === "auth" ? "Войти" : "Зарегаться"}
                </button>

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
