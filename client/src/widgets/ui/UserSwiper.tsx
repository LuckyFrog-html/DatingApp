"use client";

import { useWindowCenter } from "@/app/shared/lib/hooks/useWindowScreen";
import { useEffect, useRef, useState } from "react";
import { SwipeEventData, useSwipeable } from "react-swipeable";

type Props = {
    user: any;
    onSwipeEnded?: () => void;
    leftRightRatio: number;
    setLeftRightRatio: (val: number) => void;
};

const X_OFFSET_RATIO = 0.1;
const ROTATION_POINT_RATIO = 0.01;
const CARD_WIDTH = 300;
const CARD_HEIGHT = 500;
const CONTAINER_WIDTH = 1280;

export const UserSwiper = ({
    user,
    onSwipeEnded,
    leftRightRatio,
    setLeftRightRatio,
}: Props) => {
    const center = useWindowCenter();

    const [rotationPoint, setRotationPoint] = useState([0, 0]);
    const [rotation, setRotation] = useState(0);
    const [resXOffset, setResXOffset] = useState(0);
    const [transition, setTransition] = useState(false);
    const [yCompensation, setYCompensation] = useState(0);

    const [swipeStarted, setSwipeStarted] = useState(false);

    function changeValue(val: number, setVal: any, time: number) {
        const start = performance.now();

        function update(now: number) {
            const elapsed = now - start;
            // Вычисляем прогресс от 0 до 1
            const progress = Math.min(elapsed / time, 1);
            // Интерполируем значение от 0 до val
            const newValue = progress * val;
            setVal(newValue);
            // Если время ещё не закончилось, продолжаем анимацию
            if (progress < 1) {
                requestAnimationFrame(update);
            }
        }

        requestAnimationFrame(update);
    }

    useEffect(() => {
        // setYCompensation(-center[1]);
        setRotationPoint([center[0], center[1]]);
    }, [center]);

    const swipingHandler = (eventData: SwipeEventData) => {
        // setSwipeStarted(true);

        const e = eventData;
        const deltaMidX = eventData.initial[0] + eventData.deltaX - center[0];
        const deltaMidY = eventData.initial[1] + eventData.deltaY - center[1];

        const x = e.initial[0] + e.deltaX - center[0];
        const y = e.initial[1] + e.deltaY - center[1];
        const hypotenuse = Math.sqrt(x * x + y * y);
        let angle = Math.atan2(y, x);

        if (deltaMidX < 0) {
            if (deltaMidY < 0) {
                angle = Math.PI + angle;
            } else {
                angle = -(Math.PI - angle);
            }
        }

        const rotAngle = -angle * 0.2;

        if (rotAngle > 0) {
            setRotation(Math.min(rotAngle, 0.2));
        } else {
            setRotation(Math.max(rotAngle, -0.2));
        }
        // setRotation(-angle * 0.2);
        // if (deltaMidY > 0) {
        //     setRotation(-deltaMidX * ROTATION_POINT_RATIO);
        // } else {
        //     setRotation(deltaMidX * ROTATION_POINT_RATIO);
        // }
        // setRotationPoint([CARD_WIDTH / 2, CARD_HEIGHT / 2 + deltaMidY]);

        // console.log(x, y, y / hypotenuse);

        // const deltaBetweenCenterAndCursorX = e.initial[0] + e.deltaX;

        // const vec = [];
        // setYCompensation(-(center[1] + deltaMidY));

        setResXOffset(deltaMidX * X_OFFSET_RATIO);
        // console.log(deltaMidX, CONTAINER_WIDTH / 2, deltaMidX / (CONTAINER_WIDTH / 2), Math.min(deltaMidX / (CONTAINER_WIDTH / 2), 1), Math.max(Math.min(deltaMidX / (CONTAINER_WIDTH / 2), 1), -1));
        // if ()
        setLeftRightRatio(Math.max(Math.min(deltaMidX / (CONTAINER_WIDTH / 2), 1), -1));
    };

    const swipeStartedHandler = (eventData: SwipeEventData) => {
        // setSwipeStarted(true);
        // setTimeout(() => setSwipeStarted(false), 300);
        setTransition(false);
    };

    const swipeEndedHandler = (eventData: SwipeEventData) => {
        setTransition(true);
        setResXOffset(0);
        setRotation(0);
        setRotationPoint([CARD_WIDTH / 2, CARD_HEIGHT / 2]);
        setLeftRightRatio(0);
    };

    const handlers = useSwipeable({
        onSwipeStart: swipeStartedHandler,
        onSwiped: swipeEndedHandler,
        onSwiping: swipingHandler,
        trackMouse: true,
    });

    return (
        <div
            {...handlers}
            className="z-40 relative h-[calc(100vh-60px)] flex flex-col items-center justify-center">
            <div
                style={{
                    transform: `translateX(${resXOffset}px)`,
                    transition: transition ? "all 0.3s ease" : "none",
                }}
                // className="bg-green-500"
            >
                {/* Inner container: applies rotation with custom transform origin */}
                <div
                    style={{
                        transform: `rotate(${rotation}rad)`,
                        transformOrigin: `${rotationPoint[0]}px ${rotationPoint[1]}px`,
                        transition: transition ? "all 0.3s ease" : "none",
                    }}
                    className={`h-[${CARD_HEIGHT}px] w-[${CARD_WIDTH}px] rounded-2xl bg-blue-500`}
                />
            </div>
            {/* <div
                style={{
                    transform: `translateX(${rotationPoint[0]}px) translateY(${rotationPoint[1]}px)`,
                }}
                className="w-[1px] h-[1px] bg-amber-200 z-20"></div> */}
        </div>
    );
};
