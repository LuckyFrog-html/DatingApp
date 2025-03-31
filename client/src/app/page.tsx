import { LinkButton } from "./shared/ui/LinkButton";
import { MainHeart } from "./features/ui/MainHeart";

export default function Home() {
    return (
        <div className="flex flex-col gap-10 pt-10 items-center">
            <h1 className="text-[50px] font-normal">
                Найди <span className="font-medium text-gray-500">сво</span>ю
                любовь!
            </h1>
            <MainHeart />
            <LinkButton href="/search">Поехали!</LinkButton>
        </div>
    );
}
