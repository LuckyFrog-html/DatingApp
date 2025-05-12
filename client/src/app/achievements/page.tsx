import { RippleCard } from "@/widgets/ui/Ripple/RippleCard";
import { useMemo } from "react";

const achievements = [
    {
        id: 1,
        title: "Получил первый рейтинг",
        description: "Получил первый рейтинг на сайте",
        completed: false,
    },
    {
        id: 2,
        title: "Получил первый рейтинг",
        description: "Получил первый рейтинг на сайте",
        completed: false,
    },
    {
        id: 3,
        title: "Получил первый рейтинг",
        description: "Получил первый рейтинг на сайте",
        completed: true,
    },
    {
        id: 4,
        title: "Получил первый рейтинг",
        description: "Получил первый рейтинг на сайте",
        completed: false,
    },
    {
        id: 5,
        title: "Получил первый рейтинг",
        description: "Получил первый рейтинг на сайте",
        completed: false,
    },
    {
        id: 6,
        title: "Получил первый рейтинг",
        description: "получил первый рейтинг на сайте",
        completed: true,
    },
    {
        id: 7,
        title: "Получил первый рейтинг",
        description: "Получил первый рейтинг на сайте",
        completed: false,
    },
    {
        id: 8,
        title: "Получил первый рейтинг",
        description: "Получил первый рейтинг на сайте",
        completed: true,
    },
    {
        id: 9,
        title: "Получил первый рейтинг",
        description: "Получил первый рейтинг на сайте",
        completed: false,
    },
    {
        id: 10,
        title: "Получил первый рейтинг",
        description: "Получил первый рейтинг на сайте",
        completed: false,
    },
    {
        id: 11,
        title: "Получил первый рейтинг",
        description: "получил первый рейтинг на сайте",
        completed: true,
    },
    {
        id: 12,
        title: "Получил первый рейтинг",
        description: "получил первый рейтинг на сайте",
        completed: true,
    },
    {
        id: 13,
        title: "Получил первый рейтинг",
        description: "получил первый рейтинг на сайте",
        completed: true,
    },
];

const baseGridPattern = [
    ["1/3", ""],
    ["", ""],
    ["", ""],
    ["3", "1/3"],
    ["", ""],
    ["2/4", ""],
    ["1/3", "4/6"],
    ["", ""],
    ["", ""],
    ["", "6/8"],
    ["", ""],
    ["", ""],
    ["2/4", ""],
];

const ROWS_COUNT = 7;

const getGridPattern = (count: number) => {
    const l = baseGridPattern.length;
    const res = [];

    for (let i = 0; i < count; i++) {
        const n = Math.ceil(i / l);

        const [a, b] = baseGridPattern[i % l];

        console.log(i, n, a, b);

        if (b !== "") {
            const [a1, b1] = b.split("/").map(Number);
            res.push([
                a,
                `${a1 + ROWS_COUNT * (n - 1)}/${b1 + ROWS_COUNT * (n - 1)}`,
            ]);
        } else {
            res.push([a, ""]);
        }
    }
    return res;
};

const Achievements = () => {
    const gridPattern = useMemo(() => getGridPattern(achievements.length), []);

    return (
        <div className="pb-10 pt-2">
            <h2 className="text-center my-4 text-2xl">Achievements</h2>
            <div className="w-full grid gap-4 grid-cols-3 grid-rows-2">
                {achievements.map((item, i) => (
                    <div
                        key={i}
                        className="card w-full h-full min-h-[200px] rounded-2xl bg-gray-200 hover:bg-gray-100 transition-all cursor-pointer"
                        style={{
                            gridColumn: gridPattern[i % gridPattern.length][0],
                            gridRow: gridPattern[i % gridPattern.length][1],
                        }}>
                        <RippleCard className="w-full h-full">
                            <div className="p-3 flex h-full flex-col items-center justify-center">
                                <div>{item.title}</div>
                                <div>{item.description}</div>
                            </div>
                        </RippleCard>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default Achievements;
