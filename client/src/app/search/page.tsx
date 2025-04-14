"use client";

import { UserSwiper } from "@/widgets/ui";
import { useEffect, useState } from "react";
import { twMerge } from "tailwind-merge";

import "./page.css";

const Search = () => {
    const [ratio, setRatio] = useState(0);
    const [isTransition, setIsTransition] = useState(false);

    const [clickKludge, setClickKludge] = useState(0);

    const getSizeFromRatio = (ratio: number, positive: boolean) => {
        let val;
        if (!positive) {
            val = ratio > 0 ? ratio * 100 : (Math.abs(ratio) / 3) * 100;
        } else {
            val = ratio < 0 ? -ratio * 100 : (Math.abs(ratio) / 3) * 100;
        }

        return Math.max(val, 40);
    };

    console.log(ratio);

    const onSwipeEnded = () => {
        console.log("onSwipeEnded");
        // setRatio(0);
        // setIsTransition(true);
    };

    const onSwipeSuccessEnded = () => {
        // setRatio(0);

        console.log("onSwipeSuccessEnded");
    };

    const onDislikeClick = () => {
        setClickKludge((prev) => prev + 1);

        setRatio(-1);
    };

    const onLikeClick = () => {
        setClickKludge((prev) => prev + 1);
        setRatio(1);
    };

    return (
        <div className="relative">
            <div
                className={twMerge(
                    "z-20 absolute left-0 top-[calc(50%-50px)] rounded-full w-[150px] h-[150px] bg-blue-600 text-white flex items-center justify-center arrow",
                )}
                onClick={onDislikeClick}
                style={{
                    transform: `scale(${getSizeFromRatio(ratio, true)}%)`,
                }}>
                <p className="text-3xl">Не нрав</p>
            </div>
            <UserSwiper
                clickKludge={clickKludge}
                like={onLikeClick}
                dislike={onDislikeClick}
                leftRightRatio={ratio}
                setLeftRightRatio={setRatio}
                onSwipeEnded={onSwipeEnded}
                onSwipeSuccessEnded={onSwipeSuccessEnded}
                user={"sad"}></UserSwiper>
            <div
                className={twMerge(
                    "z-20 absolute right-0 top-[calc(50%-50px)] rounded-full w-[150px] h-[150px] bg-red-400 text-white flex items-center justify-center arrow",
                )}
                onClick={onLikeClick}
                style={{
                    transform: `scale(${getSizeFromRatio(ratio, false)}%)`,
                }}>
                <p className="text-3xl">Нрав!</p>
            </div>
        </div>
    );
};

export default Search;
