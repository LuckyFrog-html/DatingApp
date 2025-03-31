"use client";

import { UserSwiper } from "@/widgets/ui";
import { useState } from "react";
import { twMerge } from "tailwind-merge";

const Search = () => {
    const [ratio, setRatio] = useState(0);

    const getSizeFromRatio = (ratio: number, positive: boolean) => {
        let val;
        if (!positive) {
            val = ratio > 0 ? ratio * 100 : (Math.abs(ratio) / 3) * 100;
        } else {
            val = ratio < 0 ? -ratio * 100 : (Math.abs(ratio) / 3) * 100;
        }

        return Math.max(val, 40);
    };

    console.log(getSizeFromRatio(ratio, true));

    return (
        <div className="relative">
            <div
                className={twMerge(
                    "absolute left-0 top-[calc(50%-50px)] rounded-full w-[100px] h-[100px] bg-blue-600 text-white flex items-center justify-center",
                )}
                style={{
                    transform: `scale(${getSizeFromRatio(ratio, true)}%)`,
                }}>
                <p>Не нрав</p>
            </div>
            <UserSwiper
                leftRightRatio={ratio}
                setLeftRightRatio={setRatio}
                user={"sad"}></UserSwiper>
            <div
                className={twMerge(
                    "absolute right-0 top-[calc(50%-50px)] rounded-full w-[100px] h-[100px] bg-red-400 text-white flex items-center justify-center",
                )}
                style={{
                    transform: `scale(${getSizeFromRatio(ratio, false)}%)`,
                }}>
                <p>Нрав!</p>
            </div>
        </div>
    );
};

export default Search;
