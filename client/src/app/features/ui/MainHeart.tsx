import Image from "next/image";
import heart from "../../../assets/icons/heart.svg";

export const MainHeart = () => {
    return (
        <div className="grid place-items-center relative w-[300px] pulse">
            <Image src={heart} width={300} height={269} alt="heart" />
            <p className="absolute text-[20px] text-center">
                (и давай давай повышай
                <br /> демографию
                <br /> дружок)
            </p>
        </div>
    );
};
